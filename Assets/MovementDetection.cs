using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetection : MonoBehaviour
{
    public float minRadius = 1f; // The minimum radius of the circular path to detect.
    public float maxRadiusDistance = 0.4f;
    public float perpendicularThreshold = 0.1f;
    public int bufferLength = 30; // The length of the buffer to store previous positions.
    public float detectionTimeOut = 3f;
    public float minDistanceBetweenPoints = 0.1f;

    private float timeBetweenTracking; // The cooldown period in seconds before calculating circularity.
    [SerializeField]
    private Vector3[] positionBuffer;
    private int bufferIndex;
    private float lastCalculationTime;
    private float startDetectionTime;
    public GameObject pointPrefab;

    public bool debug = false;
    void Start()
    {
        ResetDetection();
    }

    private void OnEnable()
    {
        startDetectionTime = Time.time;
        ResetDetection();
    }

    void ResetDetection()
    {
        timeBetweenTracking = detectionTimeOut / bufferLength;
        positionBuffer = new Vector3[bufferLength];
        bufferIndex = 0;
        lastCalculationTime = Time.time;
    }

    void Update()
    {
        //Disable if detection timed out
        if (Time.time - startDetectionTime >= detectionTimeOut)
        {
            if (debug) Debug.Log("Detection Timeout");
            this.gameObject.SetActive(false);
        }

        // If the timeBetweenTracking period has elapsed, calculate shapes from the position buffer.
        if (Time.time - lastCalculationTime > timeBetweenTracking) // && isDetectionActive)
        {
            //Verify distance between points
            bool canAddPoint = true;
            foreach (Vector3 position in positionBuffer)
            {
                if (transform.position == Vector3.zero || Vector3.Distance(transform.position, position) < minDistanceBetweenPoints)
                {
                    canAddPoint = false;
                }
            }

            // Store the current position in the buffer.
            if (canAddPoint)
            {
                positionBuffer[bufferIndex] = transform.position;
                if (debug)
                {
                    GameObject point = Instantiate(pointPrefab, positionBuffer[bufferIndex], Quaternion.identity);
                    Destroy(point, 10);
                }
                if (bufferIndex == positionBuffer.Length - 1) //Deactivate detection for next frames after array is full
                {
                    if (debug) Debug.Log("Array is full");
                    this.gameObject.SetActive(false);
                }
                else
                {
                    bufferIndex = (bufferIndex + 1) % bufferLength;
                }
            }

            //Calculate shapes
            bool isCircular = DetectCircularData(positionBuffer);
            if (isCircular)
            {
                if (debug) Debug.Log("Circular movement detected.");
                this.gameObject.SetActive(false);
            }
            lastCalculationTime = Time.time;
        }
    }

    private bool DetectCircularData(Vector3[] points)
    {
        // Find the center of the data points
        Vector3 center = Vector3.zero;
        int pointsSize = 0;
        foreach (Vector3 point in points)
        {
            if (point != Vector3.zero)
            {
                center += point;
                pointsSize++;
            }
        }
        center /= pointsSize;

        // Calculate the average radius of the data points from the center
        float radius = 0f;
        foreach (Vector3 point in points)
        {
            if (point != Vector3.zero)
            {
                radius += Vector3.Distance(center, point);
            }
        }
        radius /= pointsSize;
        if (debug) Debug.Log("Radius: " + radius);
        if (radius < minRadius) return false;

        // Check if all data points lie on a plane perpendicular to the center
        Vector3 normal = Vector3.zero;
        foreach (Vector3 point in points)
        {

            normal += Vector3.Cross(center - point, Vector3.up);
        }
        normal.Normalize();
        if (debug) Debug.Log("Radius: " + radius + " Center: " + center + " Normal: " + normal);
        foreach (Vector3 point in points)
        {
            if (point != Vector3.zero)
            {
                float perpendicularValue = Vector3.Dot(normal, point - center);
                if (perpendicularValue > perpendicularThreshold || perpendicularValue < -perpendicularThreshold)
                {
                    if (debug) Debug.Log("Not perpendicular: " + perpendicularValue + " " + point);
                    return false;
                }
            }
        }

        // Check if all data points lie on a circle with center and radius
        foreach (Vector3 point in points)
        {
            if (point != Vector3.zero)
            {
                float distance = Vector3.Distance(center, point);
                if (distance > radius + maxRadiusDistance || distance < radius - maxRadiusDistance)
                {
                    if (debug) Debug.Log("Different Radius: " + distance + " " + point);
                    return false;
                }
            }
        }

        return true;
    }

}

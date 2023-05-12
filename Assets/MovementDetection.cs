using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetection : MonoBehaviour
{
    public int bufferLength = 30; // The length of the buffer to store previous positions.
    public float threshold = 0.1f; // The threshold for circularity.
    public float minRadius = 1f; // The minimum radius of the circular path to detect.
    public float minMoveDistance = 0.1f;
    public float detectionTimeOut = 3f;
    public bool isDetectionActive = false;

    [SerializeField]
    public float cooldown; // The cooldown period in seconds before calculating circularity.
    [SerializeField]
    private Vector3[] positionBuffer;
    private int bufferIndex;
    private bool isMoving;
    private float lastCalculationTime;
    private float startDetectionTime;
    public GameObject pointPrefab;
    void Start()
    {
        ResetDetection();
    }

    private void OnEnable()
    {
        startDetectionTime = Time.time;
    }

    void ResetDetection()
    {
        cooldown = detectionTimeOut / bufferLength;
        positionBuffer = new Vector3[bufferLength];
        bufferIndex = 0;
        isMoving = false;
        lastCalculationTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startDetectionTime >= detectionTimeOut) this.gameObject.SetActive(false);
        // If the cooldown period has elapsed, calculate the circularity of the position buffer.
        if (Time.time - lastCalculationTime > cooldown) // && isDetectionActive)
        {
            // Check if the object is moving.
            float distance = Vector3.Distance(transform.position, positionBuffer[bufferIndex == 0 ? bufferIndex : bufferIndex-1]);
            isMoving = (distance > minMoveDistance); // Adjust this threshold value as needed.
            if (!isMoving)
            {
                ResetDetection();
                return;
            }

            // Store the current position in the buffer.
            positionBuffer[bufferIndex] = transform.position;
            GameObject point = Instantiate(pointPrefab, positionBuffer[bufferIndex], Quaternion.identity);

            bool isCircular = IsCircular(positionBuffer, threshold, minRadius);
            if (isCircular && isMoving)
            {
                Debug.Log("Circular movement detected.");
                ResetDetection();
                //isDetectionActive = false;
                this.gameObject.SetActive(false);
            }
            lastCalculationTime = Time.time;
            if (bufferIndex == positionBuffer.Length-1) //Deactivate detection for next frames after array is full
            {
                //isDetectionActive = false;
                ResetDetection();
                this.gameObject.SetActive(false);
            } else
            {
                bufferIndex = (bufferIndex + 1) % bufferLength;
            }
        }
    }

    bool IsCircular(Vector3[] positions, float threshold, float minRadius)
    {
        // Calculate the center of the positions.
        Vector3 center = Vector3.zero;
        int positionsSize = 0;
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i] != Vector3.zero) 
            {
                center += positions[i];
                positionsSize++;
            }
        }
        Debug.Log(center);
        //center /= positions.Length;
        center /= positionsSize;
        Debug.Log(center);
        // Calculate the average distance from the center.
        float radius = 0;
        for (int i = 0; i < positions.Length; i++)
        {
            radius += Vector3.Distance(center, positions[i]);
        }
        radius /= positions.Length;

        // Calculate the standard deviation of the distance from the center.
        float variance = 0;
        for (int i = 0; i < positions.Length; i++)
        {
            variance += Mathf.Pow(Vector3.Distance(center, positions[i]) - radius, 2);
        }
        variance /= positions.Length;
        float standardDeviation = Mathf.Sqrt(variance);

        // Check if the positions form a circular path with a minimum radius.
        bool isCircular = (standardDeviation < threshold && radius >= minRadius);
        return isCircular;
    }
}

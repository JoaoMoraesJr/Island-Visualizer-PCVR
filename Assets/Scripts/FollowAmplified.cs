using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAmplified : MonoBehaviour
{
    public Transform target;             // The object to predict its movement
    public float predictionTime = 1f;    // Time ahead to predict the movement
    public float minMovementDistance = 0.1f;

    private Vector3 lastTargetPosition;  // Previous position of the target object
    private Vector3 predictedPosition;

    private void Start()
    {
        lastTargetPosition = target.position;
        predictedPosition = target.position;
    }

    private void Update()
    {
        if (target != null)
        {
            // Predict the target position using the current and previous positions
            Vector3 velocity = (target.position - lastTargetPosition) / Time.deltaTime;
            //if (velocity.magnitude > minMovementDistance)
            //{
                predictedPosition = target.position + velocity * predictionTime;
                predictedPosition.y = Mathf.Clamp(predictedPosition.y, 0, 1);
            //}

            // Calculate the direction towards the predicted position
            Vector3 direction = (predictedPosition - transform.position).normalized;

            // Rotate towards the direction
            //transform.rotation = Quaternion.LookRotation(direction);

            // Move towards the predicted position using Lerp for smooth movement
            transform.position = Vector3.Lerp(transform.position, predictedPosition, Time.deltaTime);

            // Update the last position
            lastTargetPosition = target.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAmplified : MonoBehaviour
{
    public Transform target;             // The object to predict its movement
    public float predictionTime = 1f;    // Time ahead to predict the movement

    private Vector3 lastTargetPosition;  // Previous position of the target object

    private void Start()
    {
        lastTargetPosition = target.position;
    }

    private void Update()
    {
        if (target != null)
        {
            // Predict the target position using the current and previous positions
            Vector3 velocity = (target.position - lastTargetPosition) / Time.deltaTime;
            if (velocity != Vector3.zero)
            {
                Vector3 predictedPosition = target.position + velocity * predictionTime;

                // Calculate the direction towards the predicted position
                Vector3 direction = (predictedPosition - transform.position).normalized;

                // Rotate towards the direction
                transform.rotation = Quaternion.LookRotation(direction);

                // Move towards the predicted position using Lerp for smooth movement
                transform.position = Vector3.Lerp(transform.position, predictedPosition, Time.deltaTime);

                // Update the last position
                lastTargetPosition = target.position;

            }
        }
    }
}

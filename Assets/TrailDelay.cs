using UnityEngine;

public class TrailDelay : MonoBehaviour
{
    public Transform target; // the target object to follow
    public float followLag = 0.2f; // the amount of lag in seconds
    public float followSpeed = 5.0f; // the speed at which to follow the target

    private Vector3 targetPosition; // the current position of the target
    private Vector3 targetVelocity; // the current velocity of the target
    private Vector3 predictedPosition; // the predicted position of the target
    private Vector3 currentVelocity; // the current velocity of the object

    private void FixedUpdate()
    {
        // get the target's position and calculate its velocity
        Vector3 previousTargetPosition = targetPosition;
        targetPosition = target.position;
        targetVelocity = (targetPosition - previousTargetPosition) / Time.deltaTime;

        // predict the target's position with lag
        predictedPosition = targetPosition + targetVelocity * followLag;

        // use SmoothDamp to interpolate between the current position and the predicted position
        transform.position = Vector3.SmoothDamp(transform.position, predictedPosition, ref currentVelocity, 1.0f / followSpeed);
    }
}
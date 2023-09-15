using UnityEngine;

public class FollowDelay : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5.0f;

    private void Start()
    {
        if (target == null)
        {
            target = transform;
        }
    }

    void Update()
    {
        // Calculate the desired position of this game object
        Vector3 targetPosition = target.position;

        // Move this game object towards the desired position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Set the rotation of this game object to look at the target
        //transform.LookAt(target);
    }
}

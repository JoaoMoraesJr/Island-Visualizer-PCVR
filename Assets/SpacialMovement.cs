using UnityEngine;

public class SpacialMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f; // Adjust this to control movement speed.
    public float rotationSpeed = 2.0f; // Adjust this to control rotation speed.
    public Vector2 limitX = new Vector2(490f, 560f);
    public Vector2 limitY = new Vector2(3.5f, 15f);
    public Vector2 limitZ = new Vector2(515f, 570f);
    public float delayToStartMovement = 3f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private bool allowMovement = false;
    public Transform lookAt;

    private void Start()
    {
        Invoke("startMovement", delayToStartMovement);
        rotationX = transform.rotation.eulerAngles.x;
        rotationY = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        if (!allowMovement) return;
        // Handle movement using WASD or arrow keys.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate the new rotations
        rotationY += mouseX * rotationSpeed;
        rotationX -= mouseY * rotationSpeed;

        // Clamp the vertical rotation to prevent flipping
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply the rotations to the GameObject
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);

        // Handle upward movement using the space key.
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 upMovement = Vector3.up * moveSpeed * Time.deltaTime;
            transform.Translate(upMovement);
        }

        // Handle downward movement using the left control key.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 downMovement = Vector3.down * moveSpeed * Time.deltaTime;
            transform.Translate(downMovement);
        }

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, limitX.x, limitX.y),
            Mathf.Clamp(transform.position.y, limitY.x, limitY.y),
            Mathf.Clamp(transform.position.z, limitZ.x, limitZ.y)
        );
    }

    private void startMovement()
    {
        allowMovement = true;
    }
}


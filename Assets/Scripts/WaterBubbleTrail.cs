using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaterBubbleTrail : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh originalMesh;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;
    public float deformationStrength = 0.1f;
    public float movementThreshold = 0.1f;
    public float lagTime = 0.1f;
    private float timeSinceLastMovement = 0f;
    private bool isLagging = false;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        originalMesh = meshFilter.mesh;
        originalVertices = originalMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        Vector3 movement = transform.position - (transform.position - transform.forward * 0.5f);
        float movementAmount = movement.magnitude;

        if (movementAmount > movementThreshold)
        {
            timeSinceLastMovement = 0f;
            isLagging = false;
        }
        else
        {
            timeSinceLastMovement += Time.deltaTime;
        }

        // Apply lag effect
        if (timeSinceLastMovement < lagTime)
        {
            isLagging = true;
        }

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 originalVertex = originalVertices[i];

            // Calculate displacement
            Vector3 displacement;
            if (isLagging)
            {
                displacement = displacedVertices[i] - originalVertex;
            }
            else
            {
                displacement = Vector3.zero;
            }
            Vector3 targetPosition = transform.TransformPoint(originalVertex) + displacement;
            Vector3 currentPosition = displacedVertices[i];
            Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, deformationStrength * Time.deltaTime);

            // Store the new position in the displacedVertices array
            displacedVertices[i] = newPosition;
        }

        // Update the mesh vertices with the displacedVertices array
        originalMesh.vertices = displacedVertices;
        originalMesh.RecalculateNormals();
        meshFilter.mesh = originalMesh;
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player: MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("XR Interaction Manager");

        transform.Find("Model/Left Hand").GetComponent<XRRayInteractor>().interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
        transform.Find("Model/Right Hand").GetComponent<XRRayInteractor>().interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal*0.01f, 0, moveVertical*0.01f);
        transform.position = transform.position + movement;
    }
}

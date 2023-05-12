using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : NetworkBehaviour
{
    [SerializeField] Behaviour[] disabledComponentsForNetworkPlayers;
    [SerializeField] AudioListener playerAudio;
    [SerializeField] Camera playerCamera;
    [SerializeField] bool singlePlayer = false;
    private void Start()
    {
        if (isLocalPlayer || singlePlayer)
        {
            foreach (Behaviour component in disabledComponentsForNetworkPlayers)
            {
                component.enabled = true;
            }
            playerAudio.enabled = true;
            playerCamera.enabled = true;
            GameObject.Find("XR Interaction Manager");
            transform.Find("Model/Left Hand").GetComponent<XRRayInteractor>().interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
            transform.Find("Model/Right Hand").GetComponent<XRRayInteractor>().interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
        } else
        {
            Destroy(playerAudio);
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal*0.01f, 0, moveVertical*0.01f);
            transform.position = transform.position + movement;


        }
    }
}

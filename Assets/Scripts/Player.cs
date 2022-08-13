using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] Behaviour[] disabledComponentsForNetworkPlayers;
    [SerializeField] AudioListener playerAudio;
    [SerializeField] Camera playerCamera;
    private void Start()
    {
        if (isLocalPlayer)
        {
            foreach (Behaviour component in disabledComponentsForNetworkPlayers)
            {
                component.enabled = true;
            }
            playerAudio.enabled = true;
            playerCamera.enabled = true;
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

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
        if (!isLocalPlayer)
        {
            foreach (Behaviour component in disabledComponentsForNetworkPlayers)
            {
                component.enabled = false;
            }
            playerAudio.enabled = false;
            playerCamera.enabled = false;
        }
    }

    private void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .5f);
        }
    }
}

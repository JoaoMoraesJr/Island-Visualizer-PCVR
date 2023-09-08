using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Transform playerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(true);
        player.transform.position = playerSpawn.position + new Vector3(0, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

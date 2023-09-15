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
        Invoke("setPlayerPosition", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayerPosition()
    {
        player.transform.position = playerSpawn.position + new Vector3(0, 3.5f, 0);
    }
}

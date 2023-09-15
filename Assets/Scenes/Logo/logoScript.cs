using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logoScript : MonoBehaviour
{

    public string nextSceneName = "Intro";
    public float timeDuration = 4;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", timeDuration);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public float timeToDestroy = 1f;
    public float allowedTimeToDestroyThreshold = 0.2f;
    public bool flagToDelete = false;
    private float startTime = 0;
    private float trailStartDuration;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<TrailRenderer>().time);
    }

    // Update is called once per frame
    void Update()
    {
        if (flagToDelete)
        {
            float elapsedTime = Time.time - startTime;
            GetComponent<TrailRenderer>().time = Mathf.Lerp(trailStartDuration, 0, Mathf.Clamp01(elapsedTime / timeToDestroy));
            if (elapsedTime >= timeToDestroy - allowedTimeToDestroyThreshold)
            {
                Destroy(this.gameObject, 0);
            }
        }

    }

    public void DestroyTrail()
    {
        flagToDelete = true;
        startTime = Time.time;
        trailStartDuration = GetComponent<TrailRenderer>().time;
        Debug.Log("Destroy");
    }
}

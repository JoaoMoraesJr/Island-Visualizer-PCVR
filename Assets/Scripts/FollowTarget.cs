using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform head;
    public Transform hand;
    public float targetDistance = 1f;
    public float minDistance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = head.position - hand.position;
        distance.y = 0;
        //distance.x = 0;
        //distance.z += minDistance;
        distance = distance * targetDistance;
        if (distance.z > -minDistance) distance.z = -minDistance;
        transform.position = hand.position + hand.forward * distance.magnitude;
    }
}

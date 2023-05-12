using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float speed = 2f; // speed of rotation
    public float radius = 2f; // radius of circle

    private float angle = 0f; // current angle in radians

    private void Update()
    {
        angle += speed * Time.deltaTime; // increase angle based on speed and time

        float x = Mathf.Cos(angle) * radius; // calculate x position based on current angle and radius
        float y = Mathf.Sin(angle) * radius; // calculate y position based on current angle and radius

        transform.position = new Vector3(x, y, 0f); // set object position to calculated position
    }
}


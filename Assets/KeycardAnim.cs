using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardAnim : MonoBehaviour
{
    float rotationSpeed;
    float floatAmplitude;
    float floatSpeed;
    float startingYPos;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 30.0f;
        floatAmplitude = 0.25f;
        floatSpeed = 2.0f;
        startingYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        transform.position = new Vector3(transform.position.x, startingYPos + (floatAmplitude * (float)(Math.Sin(Time.time * floatSpeed))), transform.position.z);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    bool isRunningInstructions;

    bool isMovingForwards;
    bool isTurningRight;
    bool isTurningLeft;
    bool isIdling;

    float distanceToGo;
    float degreesToGo;
    float timeLeftToIdle;

    List<Tuple<string, float>> activeInstructions;

    // Start is called before the first frame update
    void Start()
    {
        isRunningInstructions = false;
        isMovingForwards = false;
        isTurningRight = false;
        isTurningLeft = false;
        isIdling = false;
        moveSpeed = 5.0f;
        rotationSpeed = 70.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunningInstructions){
            if(isMovingForwards && distanceToGo > 0){
                Vector3 moveDirection = moveSpeed * new Vector3(0, 0, 1) * Time.deltaTime;
                distanceToGo -= moveDirection.magnitude;
                transform.Translate(moveDirection);
            }else if(isMovingForwards && distanceToGo <= 0){
                isMovingForwards = false;
                RunInstruction(activeInstructions);
            }

            if(isTurningRight && degreesToGo > 0){
                Vector3 rotateDirection = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                degreesToGo -= rotateDirection.magnitude;
                transform.Rotate(rotateDirection);
            }else if(isTurningRight && degreesToGo <= 0){
                isTurningRight = false;
                RunInstruction(activeInstructions);
            }

            if(isTurningLeft && degreesToGo > 0){
                Vector3 rotateDirection = new Vector3(0, -rotationSpeed * Time.deltaTime, 0);
                degreesToGo -= rotateDirection.magnitude;
                transform.Rotate(rotateDirection);
            }else if(isTurningLeft && degreesToGo <= 0){
                isTurningLeft = false;
                RunInstruction(activeInstructions);
            }

            if(isIdling && timeLeftToIdle > 0){
                timeLeftToIdle -= Time.deltaTime;
            }else if(isIdling && timeLeftToIdle <= 0){
                isIdling = false;
                RunInstruction(activeInstructions);
            }
        }
    }

    public void RunInstruction(List<Tuple<string, float>> instructions){
        if(instructions.Count == 0){
            isRunningInstructions = false;
            return;
        }
        activeInstructions = instructions;
        isRunningInstructions = true;
        if(instructions[0].Item1 == "Move Forward"){
            isMovingForwards = true;
            distanceToGo = instructions[0].Item2;
        }else if(instructions[0].Item1 == "Turn Right"){
            degreesToGo = instructions[0].Item2;
            isTurningRight = true;
        }else if(instructions[0].Item1 == "Turn Left"){
            degreesToGo = instructions[0].Item2;
            isTurningLeft = true;
        }else if(instructions[0].Item1 == "Idle"){
            isIdling = true;
            timeLeftToIdle = instructions[0].Item2;
        }
        instructions.RemoveAt(0);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    float moveSpeed;
    float rotationSpeed;
    CharacterController characterController;

    Coroutine playerCoroutine;

    public bool stopProgram;

    // Start is called before the first frame update
    void Start()
    {
        stopProgram = false;

        moveSpeed = 5.0f;
        rotationSpeed = 90.0f;

        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController is missing!");
        }
    }

    void Update(){
        if(stopProgram){
            StopCoroutine(playerCoroutine);
        }
    }

    IEnumerator RunProgram(List<InstructionSet> instructions)
    {
        while (instructions.Count > 0)
        {
            List<Instruction> instructionList = instructions[0].instructionList;

            if (instructions[0].type == "operation")
            {
                Instruction instruction = instructionList[0];
                float val = instruction.value;

                if (instruction.name == "Move Forward")
                {
                    while (val > 0)
                    {
                        Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
                        val -= moveDirection.magnitude;

                        // Use CharacterController to move
                        characterController.Move(moveDirection);

                        yield return null;
                    }
                }
                else if (instruction.name == "Turn Right")
                {
                    while (val > 0)
                    {
                        float rotation = rotationSpeed * Time.deltaTime;
                        val -= rotation;

                        // Rotate the bot
                        transform.Rotate(0, rotation, 0);

                        yield return null;
                    }
                }
                else if (instruction.name == "Turn Left")
                {
                    while (val > 0)
                    {
                        float rotation = rotationSpeed * Time.deltaTime;
                        val -= rotation;

                        // Rotate the bot
                        transform.Rotate(0, -rotation, 0);

                        yield return null;
                    }
                }
                else if (instruction.name == "Idle")
                {
                    yield return new WaitForSeconds(val);
                }
            }
            else if (instructions[0].type == "conditional")
            {
                if (true) // Condition handling logic
                {
                    foreach (Instruction instruction in instructionList)
                    {
                        float val = instruction.value;

                        if (instruction.name == "Move Forward")
                        {
                            while (val > 0)
                            {
                                Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
                                val -= moveDirection.magnitude;

                                characterController.Move(moveDirection);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Turn Right")
                        {
                            while (val > 0)
                            {
                                float rotation = rotationSpeed * Time.deltaTime;
                                val -= rotation;

                                transform.Rotate(0, rotation, 0);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Turn Left")
                        {
                            while (val > 0)
                            {
                                float rotation = rotationSpeed * Time.deltaTime;
                                val -= rotation;

                                transform.Rotate(0, -rotation, 0);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Idle")
                        {
                            yield return new WaitForSeconds(val);
                        }
                    }
                }
            }
            else if (instructions[0].type == "for loop")
            {
                for (int i = 0; i < instructions[0].numIterations; i++)
                {
                    foreach (Instruction instruction in instructionList)
                    {
                        float val = instruction.value;

                        if (instruction.name == "Move Forward")
                        {
                            while (val > 0)
                            {
                                Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
                                val -= moveDirection.magnitude;

                                characterController.Move(moveDirection);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Turn Right")
                        {
                            while (val > 0)
                            {
                                float rotation = rotationSpeed * Time.deltaTime;
                                val -= rotation;

                                transform.Rotate(0, rotation, 0);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Turn Left")
                        {
                            while (val > 0)
                            {
                                float rotation = rotationSpeed * Time.deltaTime;
                                val -= rotation;

                                transform.Rotate(0, -rotation, 0);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Idle")
                        {
                            yield return new WaitForSeconds(val);
                        }
                    }
                }
            }
            else if (instructions[0].type == "while loop")
            {
                while (true)
                {
                    foreach (Instruction instruction in instructionList)
                    {
                        float val = instruction.value;

                        if (instruction.name == "Move Forward")
                        {
                            while (val > 0)
                            {
                                Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
                                val -= moveDirection.magnitude;

                                characterController.Move(moveDirection);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Turn Right")
                        {
                            while (val > 0)
                            {
                                float rotation = rotationSpeed * Time.deltaTime;
                                val -= rotation;

                                transform.Rotate(0, rotation, 0);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Turn Left")
                        {
                            while (val > 0)
                            {
                                float rotation = rotationSpeed * Time.deltaTime;
                                val -= rotation;

                                transform.Rotate(0, -rotation, 0);

                                yield return null;
                            }
                        }
                        else if (instruction.name == "Idle")
                        {
                            yield return new WaitForSeconds(val);
                        }
                    }
                }
            }

            instructions.RemoveAt(0);
        }
    }

    public void RunInstruction(List<InstructionSet> instructions)
    {
        playerCoroutine = StartCoroutine(RunProgram(instructions));
    }
}

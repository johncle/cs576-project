using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    float moveSpeed;
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5.0f;
        rotationSpeed = 90.0f;
    }

    IEnumerator RunProgram(List<InstructionSet> instructions){
        while(instructions.Count > 0){
            List<Instruction> instructionList = instructions[0].instructionList;
            if(instructions[0].type == "operation"){
                Instruction instruction = instructionList[0];
                float val = instruction.value;
                if(instruction.name == "Move Forward"){
                    while(val > 0){
                        Vector3 moveDirection = moveSpeed * new Vector3(0, 0, 1) * Time.deltaTime;
                        val -= moveDirection.magnitude;
                        transform.Translate(moveDirection);
                        yield return null;
                    }
                }else if(instruction.name == "Turn Right"){
                    while(val > 0){
                        Vector3 rotateDirection = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                        val -= rotateDirection.magnitude;
                        transform.Rotate(rotateDirection);
                        yield return null;
                    }
                }else if(instruction.name == "Turn Left"){
                    while(val > 0){
                        Vector3 rotateDirection = new Vector3(0, -rotationSpeed * Time.deltaTime, 0);
                        val -= rotateDirection.magnitude;
                        transform.Rotate(rotateDirection);
                        yield return null;
                    }
                }else if(instruction.name == "Idle"){
                    yield return new WaitForSeconds(val);
                }
            }else if(instructions[0].type == "conditional"){
                if(true){
                    foreach(Instruction instruction in instructionList){
                        float val = instruction.value;
                        if(instruction.name == "Move Forward"){
                            while(val > 0){
                                Vector3 moveDirection = moveSpeed * new Vector3(0, 0, 1) * Time.deltaTime;
                                val -= moveDirection.magnitude;
                                transform.Translate(moveDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Turn Right"){
                            while(val > 0){
                                Vector3 rotateDirection = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                                val -= rotateDirection.magnitude;
                                transform.Rotate(rotateDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Turn Left"){
                            while(val > 0){
                                Vector3 rotateDirection = new Vector3(0, -rotationSpeed * Time.deltaTime, 0);
                                val -= rotateDirection.magnitude;
                                transform.Rotate(rotateDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Idle"){
                            yield return new WaitForSeconds(val);
                        }
                    }
                }
            }else if(instructions[0].type == "for loop"){
                for(int i = 0; i < instructions[0].numIterations; i++){
                    foreach(Instruction instruction in instructionList){
                        float val = instruction.value;
                        if(instruction.name == "Move Forward"){
                            while(val > 0){
                                Vector3 moveDirection = moveSpeed * new Vector3(0, 0, 1) * Time.deltaTime;
                                val -= moveDirection.magnitude;
                                transform.Translate(moveDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Turn Right"){
                            while(val > 0){
                                Vector3 rotateDirection = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                                val -= rotateDirection.magnitude;
                                transform.Rotate(rotateDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Turn Left"){
                            while(val > 0){
                                Vector3 rotateDirection = new Vector3(0, -rotationSpeed * Time.deltaTime, 0);
                                val -= rotateDirection.magnitude;
                                transform.Rotate(rotateDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Idle"){
                            yield return new WaitForSeconds(val);
                        }
                    }
                }
            }else if(instructions[0].type == "while loop"){
                while(true){
                    foreach(Instruction instruction in instructionList){
                        float val = instruction.value;
                        if(instruction.name == "Move Forward"){
                            while(val > 0){
                                Vector3 moveDirection = moveSpeed * new Vector3(0, 0, 1) * Time.deltaTime;
                                val -= moveDirection.magnitude;
                                transform.Translate(moveDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Turn Right"){
                            while(val > 0){
                                Vector3 rotateDirection = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                                val -= rotateDirection.magnitude;
                                transform.Rotate(rotateDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Turn Left"){
                            while(val > 0){
                                Vector3 rotateDirection = new Vector3(0, -rotationSpeed * Time.deltaTime, 0);
                                val -= rotateDirection.magnitude;
                                transform.Rotate(rotateDirection);
                                yield return null;
                            }
                        }else if(instruction.name == "Idle"){
                            yield return new WaitForSeconds(val);
                        }
                    }
                }
            }
            instructions.RemoveAt(0);
        }
    }

    public void RunInstruction(List<InstructionSet> instructions){
        StartCoroutine(RunProgram(instructions));
    }
}

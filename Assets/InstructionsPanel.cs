using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionsPanel : MonoBehaviour
{
    public List<Tuple<string, float>> instructions;

    public GameObject playerBot;

    void Start(){
        instructions = new List<Tuple<string, float>>();
    }

    public void OnRunProgram(){
        foreach (Transform instruction in transform){
            if(instruction.gameObject.name.Contains("Move Forward")){
                instructions.Add(new Tuple<string, float>("Move Forward", float.Parse(instruction.GetComponentInChildren<TMP_InputField>().text)));
            }else if(instruction.gameObject.name.Contains("Turn Right")){
                instructions.Add(new Tuple<string, float>("Turn Right", float.Parse(instruction.GetComponentInChildren<TMP_InputField>().text)));
            }else if(instruction.gameObject.name.Contains("Turn Left")){
                instructions.Add(new Tuple<string, float>("Turn Left", float.Parse(instruction.GetComponentInChildren<TMP_InputField>().text)));
            }else if(instruction.gameObject.name.Contains("Idle")){
                instructions.Add(new Tuple<string, float>("Idle", float.Parse(instruction.GetComponentInChildren<TMP_InputField>().text)));
            }
        }
        playerBot.GetComponent<PlayerBot>().RunInstruction(instructions);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgramPanel : MonoBehaviour
{
    public List<Tuple<string, float>> instructions;

    GameObject playerBot;

    void Start(){
        playerBot = GameObject.Find("Player Bot");
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
            }else if(instruction.gameObject.name.Contains("If Statement")){
                if(true){ // condition
                    instruction.gameObject.GetComponentInChildren<ProgramPanel>().OnRunProgram();
                }
            }
        }
        playerBot.GetComponent<PlayerBot>().RunInstruction(instructions);
    }

    float operationSpacing = 54.0f;

    public float SetOperationsSpacing(){
        float currPos = 0.0f;
        foreach (Transform operation in transform){
            operation.localPosition = new Vector3(0, -currPos, 0);
            if(operation.gameObject.tag == "Operation"){
                currPos += operationSpacing;
            }else if(operation.gameObject.tag == "Conditional"){
                Transform conditionalOperations = operation.Find("Conditional Operations");
                float spacing = conditionalOperations.GetComponent<ProgramPanel>().SetOperationsSpacing();
                currPos += spacing == 0.0f ? 90.0f : operationSpacing + spacing;

                // Update whitespace of if statement in UI
                RectTransform whitespaceBox = operation.Find("Whitespace Box").GetComponent<RectTransform>();
                whitespaceBox.sizeDelta = new Vector2(whitespaceBox.sizeDelta[0], (spacing - 35.0f) < 1 ? 1 : (spacing - 35.0f));

                // Update rect that defines where new operations can be dropped in
                RectTransform conditionalOpsBox = conditionalOperations.GetComponent<RectTransform>();
                conditionalOpsBox.sizeDelta = new Vector2(conditionalOpsBox.sizeDelta[0], 75.0f + spacing);
            }
        }
        return currPos;
    }
}

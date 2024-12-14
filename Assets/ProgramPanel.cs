using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instruction {
    public string name;
    public float value;
    public Instruction(string _name, float _value){
        name = _name;
        value = _value;
    }
}

public class InstructionSet {
    public string type;
    public List<Instruction> instructionList;
    public List<string> boolValues;
    public string boolOperator;
    public int numIterations;

    // individual operations
    public InstructionSet(string _type, List<Instruction> _instructionList){
        type = _type;
        instructionList = _instructionList;
    }

    // if statements and while loops (conditionals)
    public InstructionSet(string _type, List<Instruction> _instructionList, List<string> _boolValues, string _boolOperator){
        type = _type;
        instructionList = _instructionList;
        boolValues = _boolValues;
        boolOperator = _boolOperator;
    }

    // for loops
    public InstructionSet(string _type, List<Instruction> _instructionList, int _numIterations){
        type = _type;
        instructionList = _instructionList;
        numIterations = _numIterations;
    }
}

public class ProgramPanel : MonoBehaviour
{
    public List<InstructionSet> instructions;

    GameObject playerBot;

    public GameObject expressionBuilderPrefab;

    GameObject expressionBuilderInstance;

    GameObject canvas;

    public GameObject selectExpressionsWarning;

    void Start(){
        playerBot = GameObject.Find("Player Bot");
        canvas = GameObject.Find("Canvas");
    }

    public void BuildInstructions(){
        selectExpressionsWarning.SetActive(false);
        instructions = new List<InstructionSet>();
        List<string> operations = new List<string>{"Move Forward", "Turn Right", "Turn Left", "Idle"};
        foreach (Transform instruction in transform){ // iterate children of this object
            float val = float.Parse(instruction.GetComponentInChildren<TMP_InputField>().text);
            foreach (string operation in operations){
                if(instruction.gameObject.name.Contains(operation)){
                    instructions.Add(new InstructionSet("operation", new List<Instruction>{new Instruction(operation, val)}));
                }
            }
            if(instruction.gameObject.name.Contains("If Statement") || instruction.gameObject.name.Contains("For Statement") || instruction.gameObject.name.Contains("While Statement")){
                Transform conditionalOperations = instruction.Find("Conditional Operations");
                List<Instruction> instructionList = new List<Instruction>();
                if(instruction.gameObject.name.Contains("If Statement")){
                    GameObject expBuilder = instruction.Find("Conditional Operations").GetComponent<ProgramPanel>().expressionBuilderInstance;
                    Transform expBuilderPanel = expBuilder.transform.Find("Expression Builder Panel");
                    if(expBuilder == null || expBuilderPanel.childCount < 1){
                        selectExpressionsWarning.SetActive(true);
                        return;
                    }
                    instructions.Add(new InstructionSet("conditional", instructionList, null, null));
                }else if(instruction.gameObject.name.Contains("For Statement")){
                    instructions.Add(new InstructionSet("for loop", instructionList, int.Parse(instruction.Find("Input Field").GetComponent<TMP_InputField>().text)));
                }else if(instruction.gameObject.name.Contains("While Statement")){
                    instructions.Add(new InstructionSet("while loop", instructionList, null, null));
                }
                foreach (Transform conditionalInstruction in conditionalOperations){
                    float conditionalVal = float.Parse(conditionalInstruction.GetComponentInChildren<TMP_InputField>().text);
                    foreach (string operation in operations){
                        if(conditionalInstruction.gameObject.name.Contains(operation)){
                            instructionList.Add(new Instruction(operation, conditionalVal));
                        }
                    }
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

    public void OnExpressionClick(){
        GameObject[] expressionBuilderWindows = GameObject.FindGameObjectsWithTag("Expression Builder");
        foreach(GameObject obj in expressionBuilderWindows){
            obj.SetActive(false);
        }
        if(expressionBuilderInstance == null){
            expressionBuilderInstance = Instantiate(expressionBuilderPrefab, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
            expressionBuilderInstance.transform.localPosition = new Vector3(-350, 0, 0);
        }else{
            expressionBuilderInstance.SetActive(true);
        }
    }
}

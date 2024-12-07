using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggedInstruction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject instructionInstance;

    RectTransform operationsBounds;

    public GameObject operation;
    public GameObject draggedInstructionPanel;
    public GameObject programPanel;

    Image programHighlight;

    void Start(){
        operationsBounds = programPanel.GetComponent<RectTransform>();
        programHighlight = GameObject.Find("Program Highlight").GetComponent<Image>();
    }

    bool isHoveringRect(Vector3 position, RectTransform rectTransform){
        return rectTransform.rect.Contains(rectTransform.transform.InverseTransformPoint(position));
    }

    public void OnBeginDrag(PointerEventData e){
        instructionInstance = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, draggedInstructionPanel.transform);
    }

    public void OnDrag(PointerEventData e){
        instructionInstance.transform.position = new Vector3(e.position.x, e.position.y, 0);
        programHighlight.color = new Color32(255, 255, 255, (byte)(isHoveringRect(e.position, operationsBounds) ? 75 : 30));
    }

    public void OnEndDrag(PointerEventData e){
        Destroy(instructionInstance);
        programHighlight.color = new Color32(255, 255, 255, 0);

        if(!(operation.name.Contains("If Statement") || operation.name.Contains("For Statement") || operation.name.Contains("While Statement"))){
            foreach (Transform childOperation in programPanel.transform){
                if(childOperation.gameObject.tag == "Conditional"){
                    Transform conditionalOps = childOperation.Find("Conditional Operations");
                    if(isHoveringRect(e.position, conditionalOps.GetComponent<RectTransform>())){
                        Instantiate(operation, gameObject.transform.position, Quaternion.identity, conditionalOps);
                        programPanel.GetComponent<ProgramPanel>().SetOperationsSpacing();
                        return;
                    }
                }
            }
        }

        if(isHoveringRect(e.position, operationsBounds)){
            Instantiate(operation, gameObject.transform.position, Quaternion.identity, programPanel.transform);
            programPanel.GetComponent<ProgramPanel>().SetOperationsSpacing();
        }
    }
}

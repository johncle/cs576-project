using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionGenBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject instructionInstance;

    public GameObject InstructionsPanel;

    Vector3 savedPos;

    public void OnBeginDrag(PointerEventData e){
        savedPos = gameObject.transform.position;
        if(!gameObject.name.Contains("(Clone)")){
            instructionInstance = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, InstructionsPanel.transform);
        }
    }

    public void OnDrag(PointerEventData e){
        if(!gameObject.name.Contains("(Clone)")){
            instructionInstance.transform.position = new Vector3(e.position.x + 100, e.position.y, 0);
        }else{
            transform.position = new Vector3(e.position.x + 100, e.position.y, 0);
        }
    }

    public void OnEndDrag(PointerEventData e){
        Vector3 panelPos = InstructionsPanel.transform.position;
        if(!gameObject.name.Contains("(Clone)")){
            instructionInstance.transform.position = new Vector3(panelPos.x - 135f, panelPos.y + 430f - 59.9f * InstructionsPanel.transform.childCount);
        }else{
            transform.position = savedPos;
        }
    }
}

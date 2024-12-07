using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BooleanValueDragged : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject instructionInstance;

    RectTransform operationsBounds;

    public GameObject operation;
    public GameObject draggedValuesPanel;
    public GameObject expressionBuilderPanel;

    Image programHighlight;

    void Start(){
        operationsBounds = expressionBuilderPanel.GetComponent<RectTransform>();
        programHighlight = GameObject.Find("Expression Builder Highlight").GetComponent<Image>();
    }

    bool isHoveringRect(Vector3 position, RectTransform rectTransform){
        return rectTransform.rect.Contains(rectTransform.transform.InverseTransformPoint(position));
    }

    public void OnBeginDrag(PointerEventData e){
        instructionInstance = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, draggedValuesPanel.transform);
    }

    public void OnDrag(PointerEventData e){
        instructionInstance.transform.position = new Vector3(e.position.x, e.position.y, 0);
        programHighlight.color = new Color32(255, 255, 255, (byte)(isHoveringRect(e.position, operationsBounds) ? 75 : 30));
    }

    public void OnEndDrag(PointerEventData e){
        Destroy(instructionInstance);
        programHighlight.color = new Color32(255, 255, 255, 0);

        if(expressionBuilderPanel.transform.childCount >= 3){
            return;
        }

        if(expressionBuilderPanel.transform.childCount == 0){
            if(operation.name.Contains("Operator")){
                return;
            }else{
                if(isHoveringRect(e.position, operationsBounds)){
                    Instantiate(operation, gameObject.transform.position, Quaternion.identity, expressionBuilderPanel.transform);
                    expressionBuilderPanel.GetComponent<ExpressionBuilderPanel>().SetOperationsSpacing();
                }
            }
        }else{
            Transform lastBoolItem = expressionBuilderPanel.transform.GetChild(expressionBuilderPanel.transform.childCount - 1);

            if(isHoveringRect(e.position, operationsBounds) && (lastBoolItem.name.Contains("Operator") ^ operation.name.Contains("Operator"))){
                Instantiate(operation, gameObject.transform.position, Quaternion.identity, expressionBuilderPanel.transform);
                expressionBuilderPanel.GetComponent<ExpressionBuilderPanel>().SetOperationsSpacing();
            }
        }
    }
}

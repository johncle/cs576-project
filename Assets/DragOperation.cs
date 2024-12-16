using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragOperation : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform operationsBounds;

    void Start(){
        operationsBounds = transform.parent.GetComponent<RectTransform>();
    }

    bool isHoveringRect(Vector3 position, RectTransform rectTransform){
        return rectTransform.rect.Contains(rectTransform.transform.InverseTransformPoint(position));
    }

    Vector3 prevPos;

    public void OnBeginDrag(PointerEventData e){
        prevPos = Input.mousePosition;
    }
    
    public void OnDrag(PointerEventData e){
        transform.Translate(new Vector3(e.position.x, e.position.y, 0) - prevPos);
        prevPos = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData e){
        if(isHoveringRect(e.position, operationsBounds)){
            int operationIndex = 0;
            while(operationIndex < transform.parent.childCount){
                Transform operation = transform.parent.GetChild(operationIndex);
                if(e.position.y > operation.position.y){
                    transform.SetSiblingIndex(operationIndex - 1 >= 0 ? operationIndex - 1 : 0);
                    break;
                }
                operationIndex++;
            }
            if(operationIndex == transform.parent.childCount){
                transform.SetSiblingIndex(operationIndex - 1);
            }
            transform.parent.GetComponent<ProgramPanel>().SetOperationsSpacing();
        }else{
            transform.parent.GetComponent<ProgramPanel>().SetOperationsSpacing();
            Destroy(gameObject);
        }
    }
}

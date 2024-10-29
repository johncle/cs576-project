using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public GameObject InstructionsPanelObj;

    public void OnBeginDrag(PointerEventData e){

    }

    public void OnDrag(PointerEventData e){
        transform.position = new Vector3(e.position.x + 100, e.position.y, 0);
    }

    public void OnEndDrag(PointerEventData e){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

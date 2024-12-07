using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionBuilderPanel : MonoBehaviour
{
    float spacing = 65.0f;

    public void SetOperationsSpacing(){
        float currPos = 0.0f;
        foreach (Transform operation in transform){
            operation.localPosition = new Vector3(0, -currPos, 0);
            currPos += spacing;
        }
    }

    public void OnClose(){
        transform.parent.gameObject.SetActive(false);
    }
}

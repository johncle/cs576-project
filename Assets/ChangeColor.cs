using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject outline;

    public void OnClick(){
        outline.transform.position = transform.position;
        transform.parent.gameObject.GetComponent<KeycardColorSelect>().selectedColor = gameObject.name;
    }
}

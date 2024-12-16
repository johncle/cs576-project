using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    [SerializeField] public string keycardID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKeycard(keycardID);
                Destroy(gameObject);
            }
        }
    }
    
    Coroutine randomKeycardAnimation;

    public bool isRandomizedKeycard;

    public Material Red;
    public Material Green;
    public Material Blue;

    void Start(){
        if(isRandomizedKeycard){
            randomKeycardAnimation = StartCoroutine(randomKeycardAnim());
        }
    }

    public void stopAnimating(){
        StopCoroutine(randomKeycardAnimation);
    }

    IEnumerator randomKeycardAnim(){
        int colorIdx = 1;
        Light pointLight = gameObject.GetComponentInChildren<Light>();
        Renderer rend = gameObject.GetComponent<Renderer>();
        while(true){
            colorIdx++;
            if(colorIdx == 4){
                colorIdx = 1;
            }
            if(colorIdx == 1){
                keycardID = "Red";
                rend.material = Red;
                pointLight.color = Color.red;
                pointLight.intensity = 3.0f;
            }else if(colorIdx == 2){
                keycardID = "Green";
                rend.material = Green;
                pointLight.color = Color.green;
                pointLight.intensity = 3.0f;
            }else{
                keycardID = "Blue";
                rend.material = Blue;
                pointLight.color = Color.blue;
                pointLight.intensity = 6.0f;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}

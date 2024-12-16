using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWinPanel : MonoBehaviour
{
    public GameObject LevelWinCanvas;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            LevelWinCanvas.SetActive(true);
            StopCoroutine(other.gameObject.GetComponent<PlayerBot>().playerCoroutine);
        }
    }

    public void OnNextLevelClick(){
        SceneManager.LoadScene("Level 1");
    }

    public void OnMainMenuClick(){
        SceneManager.LoadScene("Start Menu");
    }
}

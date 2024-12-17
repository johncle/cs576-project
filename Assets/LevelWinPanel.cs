using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWinPanel : MonoBehaviour
{
    public GameObject LevelWinCanvas;

    void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player"){
            LevelWinCanvas.SetActive(true);
            other.gameObject.GetComponent<PlayerBot>().stopPlayerProgram();
        }
    }

    public void OnNextLevelClick(){
        SceneManager.LoadScene("Level 1");
    }

    public void OnMainMenuClick(){
        SceneManager.LoadScene("Start Menu");
    }
}

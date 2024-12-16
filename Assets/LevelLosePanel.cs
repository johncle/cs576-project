using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLosePanel : MonoBehaviour
{
    public GameObject LevelLoseCanvas;

    bool hasLost;

    void Start(){
        hasLost = false;
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Enemy" && !hasLost){
            LevelLoseCanvas.SetActive(true);
            gameObject.GetComponent<PlayerBot>().stopPlayerProgram();
            hasLost = true;
        }
    }

    public void OnTryAgainClick(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuClick(){
        SceneManager.LoadScene("Start Menu");
    }
}

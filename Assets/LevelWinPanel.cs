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
        string[] sceneName = SceneManager.GetActiveScene().name.Split(" ");
        int levelNum = int.Parse(sceneName[1]);
        if(levelNum == 6){
            return;
        }
        SceneManager.LoadScene("Level " + (levelNum + 1));
    }

    public void OnMainMenuClick(){
        SceneManager.LoadScene("Start Menu");
    }
}

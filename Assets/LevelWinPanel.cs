using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWinPanel : MonoBehaviour
{
    public GameObject LevelWinCanvas;
    PlayerBot playerBot;
    string nextLevelTitle; // taken from gameUI

    bool hasWon;

    void Start() {
        playerBot = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerBot>(); // to trigger player win
        if (playerBot == null)
        {
            Debug.LogError("PlayerBot is missing!");
        }

        // get next level title from public gameUI strings
        GameUI gameUI = GameObject.FindGameObjectsWithTag("GameUI")[0].GetComponent<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("gameUI is missing!");
        }
        if (gameUI.nextLevelTitle == "")
        {
            Debug.LogError("gameUI nextLevelTitle is missing!");
        }
        nextLevelTitle = gameUI.nextLevelTitle;
    }

    void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player" && !hasWon){
            LevelWinCanvas.SetActive(true);
            other.gameObject.GetComponent<PlayerBot>().stopPlayerProgram();

            playerBot.SetWin();
        }
    }

    public void OnNextLevelClick(){
        SceneManager.LoadScene(nextLevelTitle);
    }

    public void OnMainMenuClick(){
        SceneManager.LoadScene("Start Menu");
    }
}

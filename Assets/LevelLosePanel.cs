using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLosePanel : MonoBehaviour
{
    public GameObject LevelLoseCanvas;
    PlayerBot playerBot;

    bool hasLost;

    void Start(){
        hasLost = false;

        playerBot = GetComponent<PlayerBot>(); // to trigger player death
        if (playerBot == null)
        {
            Debug.LogError("PlayerBot is missing!");
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Enemy" && !hasLost){
            LevelLoseCanvas.SetActive(true);
            gameObject.GetComponent<PlayerBot>().stopPlayerProgram();
            hasLost = true;

            playerBot.TriggerDeath();
        }
    }

    public void OnTryAgainClick(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuClick(){
        SceneManager.LoadScene("Start Menu");
    }
}

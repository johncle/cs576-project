using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    bool gameIsPaused;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }else{
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            gameIsPaused = !gameIsPaused;
        }
    }
}

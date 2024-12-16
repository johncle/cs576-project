using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void HowToPlayOpen(){
        howToPlayPanel.SetActive(true);
    }

    public void HowToPlayClose(){
        howToPlayPanel.SetActive(false);
    }

    public void OnStart(){
        SceneManager.LoadScene("Level 1");
    }
}

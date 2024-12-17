using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// for syncing game ui across levels while allowing for different titles and next level
public class GameUI : MonoBehaviour
{
    public string levelTitle;
    public string levelDescription;
    public string nextLevelTitle;

    void Start()
    {
        TMP_Text titleText = GameObject.FindGameObjectsWithTag("TitleText")[0].GetComponent<TMP_Text>();
        TMP_Text titleDescription = GameObject.FindGameObjectsWithTag("TitleDescription")[0].GetComponent<TMP_Text>();
        titleText.text = levelTitle;
        titleDescription.text = levelDescription;
    }
}

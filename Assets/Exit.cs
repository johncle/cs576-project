using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private Level level;
    private bool[] keycards;

    void Start()
    {
        GameObject level_obj = GameObject.FindGameObjectWithTag("Level");
        level = level_obj.GetComponent<Level>();\
        if (level == null)
        {
            Debug.LogError("Internal error: could not find the Level object - did you remove its 'Level' tag?");
            return;
        }
        keycards = level.keycards;
    }

    void Update()
    { }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered exit zone");
        if (keycards.All(x => x))
        {
            Debug.Log("you won");
        }
        else
        {
            Debug.Log("you need to collect all items first");
        }
    }
}

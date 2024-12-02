using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adapted from assignment 5 - Drug.cs
public class Keycard : MonoBehaviour
{
    private GameObject player;
    private Level level;

    void Start()
    {
        GameObject level_obj = GameObject.FindGameObjectWithTag("Level");
        level = level_obj.GetComponent<Level>();
        if (level == null)
        {
            Debug.LogError("Internal error: could not find the Level object - did you remove its 'Level' tag?");
            return;
        }
        // level.keycards.Add();
        // fps_player_obj = level.fps_player_obj;
    }

    void Update()
    {
        Color greenness = new Color
        {
            g = Mathf.Max(1.0f, 0.1f + Mathf.Abs(Mathf.Sin(Time.time)))
        };
        GetComponent<MeshRenderer>().material.color = greenness;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PLAYER")
        {
            // level.drug_landed_on_player_recently = true;
            Destroy(gameObject);
        }
    }
}
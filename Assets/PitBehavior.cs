using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBehavior : MonoBehaviour
{
    GameObject playerBot;

    bool isDead;

    float gravityForce = -0.1f;

    Vector3 velocity;

    void Start(){
        playerBot = GameObject.Find("Player Bot");
        isDead = false;
        velocity = new Vector3(0, 0, 0);
    }

    void Update(){
        if(isDead && playerBot.transform.position.y > -9.21){
            velocity = new Vector3(velocity.x, velocity.y + (gravityForce * Time.deltaTime), velocity.z);
            playerBot.transform.Translate(velocity);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            isDead = true;
            playerBot.GetComponent<PlayerBot>().stopPlayerProgram();
            playerBot.GetComponent<PlayerBot>().TriggerDeath();
        }
    }
}

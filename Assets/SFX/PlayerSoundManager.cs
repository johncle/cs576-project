using System;
using System.Collections;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioClip startMovingSound;
    public AudioClip movingSound;
    public AudioClip stopMovingSound;
    public AudioClip turningSound;
    public AudioClip deathSound;

    AudioSource moveAudioSource;
    AudioSource turnAudioSource;
    PlayerBot playerBot;
    bool hasStartedMoving = false;
    bool hasStartedTurning = false;
    bool hasDied = false; // to stop death sound from repeating

    void Start()
    {
        playerBot = GetComponent<PlayerBot>(); // track movement with public bools
        if (playerBot == null)
        {
            Debug.LogError("PlayerBot is missing!");
        }
        moveAudioSource = transform.Find("MovementAudio").GetComponent<AudioSource>();
        if (moveAudioSource == null)
        {
            Debug.LogError("Movement AudioSource is missing!");
        }
        turnAudioSource = transform.Find("TurningAudio").GetComponent<AudioSource>();
        if (turnAudioSource == null)
        {
            Debug.LogError("Turning AudioSource is missing!");
        }

        moveAudioSource.playOnAwake = false;
        moveAudioSource.loop = true;
        moveAudioSource.clip = movingSound;

        turnAudioSource.playOnAwake = false;
        turnAudioSource.loop = true;
        turnAudioSource.clip = turningSound;
    }

    void Update()
    {
        if (playerBot.isDead && !hasDied)
        {
            StopMovingSound();
            StopTurningSound();
            PlayDeathSound();
            hasDied = true;
        }

        // dont play if dead
        if (!hasDied)
        {
            if (playerBot.isMoving)
            {
                // dont play moving sound until start sound has finished
                if (!hasStartedMoving)
                {
                    StartCoroutine(PlayStartMovingSound());
                    hasStartedMoving = true;
                    Debug.Log("started moving");
                }
            }
            else
            {
                if (hasStartedMoving)
                {
                    StopMovingSound();
                    PlayStopMovingSound();
                    hasStartedMoving = false;
                    Debug.Log("stopped moving");
                }

            }

            if (playerBot.isTurning)
            {
                if (!hasStartedTurning)
                {
                    PlayTurningSound();
                    hasStartedTurning = true;
                    Debug.Log("started turning");
                }
            }
            else
            {
                if (hasStartedTurning)
                {
                    StopTurningSound();
                    hasStartedTurning = false;
                    Debug.Log("stopped turning");
                }
            }
        }
    }

    // play "start moving" sound and wait until it is finished before playing "moving" sound
    IEnumerator PlayStartMovingSound()
    {
        moveAudioSource.PlayOneShot(startMovingSound);
        yield return new WaitForSeconds(startMovingSound.length);
        PlayMovingSound();
    }

    void PlayMovingSound()
    {
        moveAudioSource.clip = movingSound;
        moveAudioSource.Play();
    }

    void StopMovingSound()
    {
        moveAudioSource.Stop();
    }

    void PlayStopMovingSound()
    {
        moveAudioSource.PlayOneShot(stopMovingSound);
    }

    void PlayTurningSound()
    {
        turnAudioSource.clip = turningSound;
        turnAudioSource.Play();
    }

    void StopTurningSound()
    {
        turnAudioSource.Stop();
    }

    void PlayDeathSound()
    {
        moveAudioSource.PlayOneShot(deathSound);
    }
}

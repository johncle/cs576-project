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
    bool hasDied = false; // to stop death condition from repeating
    bool hasWon = false; // to stop win condition from repeating

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

        if (playerBot.hasWon && !hasWon)
        {
            StopMovingSound();
            StopTurningSound();
            PlayStopMovingSound();
            hasWon = true;
        }

        // dont play if dead or won
        if (!hasDied && !playerBot.hasWon)
        {
            if (playerBot.isMoving)
            {
                // dont play moving sound until start sound has finished
                if (!hasStartedMoving)
                {
                    StartCoroutine(PlayStartMovingSound());
                    hasStartedMoving = true;
                }
            }
            else
            {
                if (hasStartedMoving)
                {
                    StopMovingSound();
                    PlayStopMovingSound();
                    hasStartedMoving = false;
                }

            }

            if (playerBot.isTurning)
            {
                if (!hasStartedTurning)
                {
                    PlayTurningSound();
                    hasStartedTurning = true;
                }
            }
            else
            {
                if (hasStartedTurning)
                {
                    StopTurningSound();
                    hasStartedTurning = false;
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

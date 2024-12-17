using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    float moveSpeed;
    float rotationSpeed;
    CharacterController characterController;
    Animator animator;
    public bool isMoving;
    public bool isTurning;
    public bool isDead = false; // set by LevelLosePanel.cs via TriggerDeath()
    public bool hasWon = false; // set by LevelWinPanel.cs via SetWin()

    Coroutine playerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5.0f;
        rotationSpeed = 90.0f;

        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController is missing!");
        }

        GameObject[] playerPrefab = GameObject.FindGameObjectsWithTag("PlayerPrefab");
        if (playerPrefab.Length == 0)
        {
            Debug.LogError("Player prefab (JR-1 mod1...) is missing!");
        }
        animator = playerPrefab[0].GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("PlayerAnimator in prefab is missing!");
        }
    }

    public void stopPlayerProgram()
    {
        StopCoroutine(playerCoroutine);
    }

    IEnumerator RunProgram(List<InstructionSet> instructions)
    {
        while (instructions.Count > 0)
        {
            List<Instruction> instructionList = instructions[0].instructionList;

            if (instructions[0].type == "operation")
            {
                Instruction instruction = instructionList[0];
                yield return RunInstructionHelper(instruction);
            }
            else if (instructions[0].type == "conditional")
            {
                if (true) // Condition handling logic
                {
                    foreach (Instruction instruction in instructionList)
                    {
                        yield return RunInstructionHelper(instruction);
                    }
                }
            }
            else if (instructions[0].type == "for loop")
            {
                for (int i = 0; i < instructions[0].numIterations; i++)
                {
                    foreach (Instruction instruction in instructionList)
                    {
                        yield return RunInstructionHelper(instruction);
                    }
                }
            }
            else if (instructions[0].type == "while loop")
            {
                while (true)
                {
                    foreach (Instruction instruction in instructionList)
                    {
                        yield return RunInstructionHelper(instruction);
                    }
                }
            }

            instructions.RemoveAt(0);

            // idle animation after performing all instructions
            isMoving = false;
            isTurning = false;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public void RunInstruction(List<InstructionSet> instructions)
    {
        playerCoroutine = StartCoroutine(RunProgram(instructions));
    }

    IEnumerator RunInstructionHelper(Instruction instruction)
    {
        float val = instruction.value;
        if (instruction.name == "Move Forward")
        {
            // move forwards animation
            isMoving = true;
            isTurning = false;
            animator.SetBool("isMoving", isMoving);

            while (val > 0)
            {
                Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
                val -= moveDirection.magnitude;

                // Use CharacterController to move
                characterController.Move(moveDirection);

                yield return null;
            }
        }
        else if (instruction.name == "Turn Right")
        {
            // idle animation (bot doesnt move horizontally when turning)
            isMoving = false;
            isTurning = true;
            animator.SetBool("isMoving", isMoving);

            while (val > 0)
            {
                float rotation = rotationSpeed * Time.deltaTime;
                val -= rotation;

                // Rotate the bot
                transform.Rotate(0, rotation, 0);

                yield return null;
            }
        }
        else if (instruction.name == "Turn Left")
        {
            // idle animation (bot doesnt move horizontally when turning)
            isMoving = false;
            isTurning = true;
            animator.SetBool("isMoving", isMoving);

            while (val > 0)
            {
                float rotation = rotationSpeed * Time.deltaTime;
                val -= rotation;

                // Rotate the bot
                transform.Rotate(0, -rotation, 0);

                yield return null;
            }
        }
        else if (instruction.name == "Idle")
        {
            // idle animation
            isMoving = false;
            isTurning = false;
            animator.SetBool("isMoving", isMoving);

            yield return new WaitForSeconds(val);
        }
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("death");
        isDead = true;
    }

    public void SetWin()
    {
        isMoving = false;
        isTurning = false;
        animator.SetBool("isMoving", isMoving);
        hasWon = true;
    }
}

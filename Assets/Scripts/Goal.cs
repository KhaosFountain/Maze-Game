using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private bool playerReachedGoal = false;
    private bool agentReachedGoal = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerReachedGoal = true;
            CheckWinner();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            agentReachedGoal = true;
            CheckWinner();
        }
    }

    void CheckWinner()
    {
        if (playerReachedGoal)
        {
            Debug.Log("Player Wins!");
        }
        else if (agentReachedGoal)
        {
            Debug.Log("Agent Wins!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Goal : MonoBehaviour
{
    public bool isPlayer1Goal;
    public MatchController matchController; // Reference to your MatchController
    public GoalCelebration1 goalCele1;
    public GoalCelebration2 goalCele2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayer1Goal)
            {
                goalCele1.TriggerParticles();
                Debug.Log("Player 2 Scored...");
                GameObject.Find("GameManager").GetComponent<GameManager>().Player2Scored();
            }
            else
            {
                goalCele2.TriggerParticles();
                Debug.Log("Player 1 Scored...");
                GameObject.Find("GameManager").GetComponent<GameManager>().Player1Scored();
            }

            // Call GoalScored() from the MatchController
            matchController.GoalScored();
        }
    }
}

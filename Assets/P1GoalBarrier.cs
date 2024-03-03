using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class P1GoalBarrier : MonoBehaviour
{
    public bool isPlayer1GoalBarrier;
    public MatchController matchController; // Reference to your MatchController
    public CinemachineVirtualCamera goalCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            goalCam.Priority = 14;
            Time.timeScale = 0.2f; // 20% speed
            Invoke("ResetCam", 1);
        }
    }

    private void ResetCam()
    {
        goalCam.Priority = 7;
        Time.timeScale = 1f;
    }
}

using System.Collections;
using UnityEngine;
using TMPro;

public class MatchTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float initialDuration; // Initial duration in seconds
	public MatchController MatchController;

    private float currentDuration;
    private bool isPaused;

    private void Start()
    {
        float savedMatchLength = PlayerPrefs.GetFloat("MatchLength", 120f); // Default to 5 minutes if not set

        initialDuration = savedMatchLength;
        currentDuration = savedMatchLength;
        Debug.Log(PlayerPrefs.GetFloat("Matchlength"));
        isPaused = true;
        UpdateTimerUI();
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (currentDuration > 0)
        {
            if (!isPaused)
            {
                yield return new WaitForSeconds(1f);
                currentDuration--;
                UpdateTimerUI();
            }
            else
            {
                yield return null; // Wait for the next frame
            }
        }
		
		TimerReachedZeroFunction();
        // Timer is up
        // Perform end-of-timer actions
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentDuration / 60);
        int seconds = Mathf.FloorToInt(currentDuration % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }
	
	public void TimerReachedZeroFunction()
	{
		MatchController.EndMatch();
	}
	
}

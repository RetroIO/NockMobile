using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MatchController : MonoBehaviour
{
	//gameplay components
    public GameManager GameManager;
    public float countdownDuration = 3f; // Countdown duration in seconds
    public MatchTimer matchTimer; // Reference to the MatchTimer script

    //shader materials
    public Material tornadoGrid; // Assign this Material in the Unity Editor
    public float defualtGridAlpha = 0f;
    public float subtractedGridAlpha = 1f;

    public Material tornadoGlow;
    public float defualtGlowAlpha = 0.93f;
    public float subtractedGlowAlpha = 1f;

    //gameplay objects
    public GameObject Ball;
    public GameObject Opponent;
    public GameObject Player;
    public GameObject Tornado;

	//text elements
    public TextMeshProUGUI BackText;
    public TextMeshProUGUI CountdownText; // Reference to the countdown UI text
    public PlayerIntro PlayerIntro;
	public TextMeshProUGUI WinnerText;

	//ai
	public bool EnableAI = false;
    public StateManager stateManager;
    public PushingState pushingState;
    public AIController AIController;


    //cameras
    public CinemachineVirtualCamera gameCameraMain;


    public Vector2 GetBallPosition()
    {
        if (Ball != null)
        {
            return Ball.transform.position;
        }
        else
        {
            Debug.LogWarning("Ball is not instantiated yet.");
            return Vector2.zero;
        }
    }
	
	
	private Vector3 opponentStartPosition;
	private Vector3 playerStartPosition;

    private void Start()
    {
        tornadoGlow.SetFloat("_AlphaAmount", defualtGlowAlpha);
        tornadoGrid.SetFloat("_GridAlphaAmount", defualtGridAlpha);
        Ball = GameObject.FindGameObjectWithTag("Ball"); // Find the ball GameObject
        if (Ball == null)
        {
            Debug.LogError("Ball GameObject not found in the scene!");
            return;
        }
		Ball.transform.position = new Vector3(0f, 3f, 0f); // Set ball's position
		Rigidbody2D ballRigidbody = Ball.GetComponent<Rigidbody2D>();
		ballRigidbody.isKinematic = true;
		opponentStartPosition = Opponent.transform.position;
		playerStartPosition = Player.transform.position;
		EnableAI = false;
		WinnerText.gameObject.SetActive(false);
		Debug.Log("match invoked");
        Invoke("StartMatch", 5);
		
		//Animate showing of playuerjnanmes
		
    }

    public void StartMatch()
    {
		EnableAI = false;
        StartCoroutine(MatchStart());
		PlayerIntro.HidePlayerIntro();
    }

    private IEnumerator MatchStart()
    {
        // Reset scores
        GameManager.ResetScores();
		
		matchTimer.PauseTimer();

        tornadoGlow.SetFloat("_AlphaAmount", defualtGlowAlpha);
        tornadoGrid.SetFloat("_GridAlphaAmount", defualtGridAlpha);


        // Show countdown
        CountdownText.text = "3";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "2";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "1";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "GO";
        yield return new WaitForSeconds(1f);
        CountdownText.gameObject.SetActive(false); // Hide countdown text

        StartCoroutine(ScaleTornadoSmoothly());

        Rigidbody2D ballRigidbody = Ball.GetComponent<Rigidbody2D>();
		ballRigidbody.isKinematic = false;

		EnableAI = true;
		
		matchTimer.ResumeTimer();
        
    }

    
	
	public bool IsBallSpawned()
    {
        return Ball != null;
    }

    public void GoalScored()
    {
		//  start goal score celebration coroutine
		matchTimer.PauseTimer();
        EnableAI = false;
        StartCoroutine(GoalCelebration());
    }

	private IEnumerator GoalCelebration()
	{
        //  partical effects handled in the goal script on goal objects
        
        Debug.Log("particlka");
        Rigidbody2D ballRigidbody = Ball.GetComponent<Rigidbody2D>();
        ballRigidbody.velocity = Vector2.zero;
        ballRigidbody.angularVelocity = 0f;
        //play goal scored music
        Debug.Log("music");
        //zoom out on camera
        Debug.Log("camera");
        // display name of player that scored
        yield return new WaitForSeconds(1f);
        Debug.Log("scoreer name");
        // Show countdown movethis to goal celbration coroutine
        StartCoroutine(CountdownToKickoff());
    }

    private void ResetGameObjects()
	{
        gameCameraMain.Priority = 11;
        Ball.transform.position = new Vector3(0f, 3f, 0f); // Set ball's position

		Player.transform.position = playerStartPosition;
		Rigidbody2D playerRigidbody = Player.GetComponent<Rigidbody2D>();
		playerRigidbody.velocity = Vector2.zero;
		
		// Reset ball's velocity and angular velocity
		Rigidbody2D ballRigidbody = Ball.GetComponent<Rigidbody2D>();
		ballRigidbody.velocity = Vector2.zero;
		ballRigidbody.angularVelocity = 0f;
		ballRigidbody.isKinematic = true;
		EnableAI = false;
		// Restart match timer
		
	}

	private IEnumerator CountdownToKickoff()
	{
        stateManager.ResetToInitialState();
        ResetGameObjects();
        ResetOpponentPosition();

        tornadoGlow.SetFloat("_AlphaAmount", defualtGlowAlpha);
        tornadoGrid.SetFloat("_GridAlphaAmount", defualtGridAlpha);

        // Show countdown
        CountdownText.gameObject.SetActive(true);
		CountdownText.text = "3";
		yield return new WaitForSeconds(1f);
		CountdownText.text = "2";
		yield return new WaitForSeconds(1f);
		CountdownText.text = "1";
		yield return new WaitForSeconds(1f);
		CountdownText.text = "GO";
		yield return new WaitForSeconds(1f);
		CountdownText.gameObject.SetActive(false); // Hide countdown text

        StartCoroutine(ScaleTornadoSmoothly());

        Rigidbody2D ballRigidbody = Ball.GetComponent<Rigidbody2D>();
		ballRigidbody.isKinematic = false;
		
		EnableAI = true;
		

		matchTimer.ResumeTimer();
	}

	private void ResetOpponentPosition()
    {
        // Move the opponent back to its starting position
        Opponent.transform.position = opponentStartPosition;
        Rigidbody2D opponentRigidbody = Opponent.GetComponent<Rigidbody2D>();
        opponentRigidbody.velocity = Vector2.zero;
    }



    public void EndMatch()
    {	
		WinnerText.gameObject.SetActive(true);
        string winner = GameManager.GetWinner();
		Debug.Log("Winner: " + winner);
		WinnerText.text = (winner);
		StartCoroutine(ReturnToMenu());
		Debug.Log("Return called");
    }
	
	public IEnumerator ReturnToMenu()
	{	
		Debug.Log("Return started");
		BackText.text = "Returning to Menu";
		yield return new WaitForEndOfFrame(); // Wait for the UI to update
		yield return new WaitForSeconds(3f);
		BackText.text = "3";
		yield return new WaitForSeconds(1f);
		BackText.text = "2";
		yield return new WaitForSeconds(1f);
		BackText.text = "1";
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("MainMenu");
	}

    private IEnumerator ScaleTornadoSmoothly()
    {
        
        Debug.Log("Tornado hide started");

        Vector3 initialScale = new Vector3(1f, 1f, 1f);
        Vector3 finalScale = new Vector3(2.1f, 2.1f, 2.1f);

        float lerpTime = 2f; // Adjust this value to control the duration of the scale change
        float elapsedTime = 0f;

        while (elapsedTime < lerpTime)
        {
            float scaleProgress = elapsedTime / lerpTime;
            Tornado.transform.localScale = Vector3.Lerp(initialScale, finalScale, scaleProgress);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        tornadoGlow.SetFloat("_AlphaAmount", subtractedGlowAlpha);
        tornadoGrid.SetFloat("_GridAlphaAmount", subtractedGridAlpha);
        // Ensure that the final scale is set exactly
        Tornado.transform.localScale = finalScale;

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
  [Header("Ball")]
  public GameObject Ball;
  
  [Header("Player 1")]
  public GameObject Player;
  public GameObject YourGoal;
  
  [Header("Opponent")]
  public GameObject OppGoal;
  
  [Header("Score UI")]
  public GameObject P1score;
  public GameObject P2score;
  
  private int Player1Score;
  private int Player2Score;
  
	private List<string> aiNames = new List<string>
	  {
		  "mxweas",
		  "thewaterbear",
		  "jakecataford",
		  "seep",
		  "Kicker_007",
		  "jeff",
		  "towellie",
		  "clane",
		  "mango",
		  "emmitplays",
		  "mufasa",
		  "hubert",
		  "hub",
		  "Barbara Dwyer",
		  "Angl_e",
		  "Treesa"
	  };
  
	private void Start()
    {
        //start
    }
	
	public string GetRandomAIName()
    {
        if (aiNames.Count > 0)
        {
            int randomIndex = Random.Range(0, aiNames.Count);
            return aiNames[randomIndex];
        }
        else
        {
            return "Clip";
        }
    }


  
	public void ResetScores()
	{
		Player1Score = 0;
		Player2Score = 0;
	}
  
  
	public void Player1Scored()
	{
		Player1Score++;
		P1score.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
	}
  
	public void Player2Scored()
		{
			Player2Score++;
			P2score.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
		}
  
	public static string GetPlayerName()
		{
			// Change "PlayerName" to the actual PlayerPrefs key you're using
			return PlayerPrefs.GetString("PlayerName", "DefaultName");
		}
		
	public string GetWinner()
	{
		if (Player1Score > Player2Score)
		{
			return (GetPlayerName() + "WINS!!");
		}
		else if (Player2Score > Player1Score)
		{
			return "AI WINS!!";
		}
		else
		{
			return "DRAW";
		}
	}

	

}

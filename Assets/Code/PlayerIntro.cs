using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerIntro : MonoBehaviour
{   
    public TextMeshProUGUI vsText;
    public GameManager gameManager; // Make sure this is assigned in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayPlayers());
    }

    private IEnumerator DisplayPlayers()
    {
        string playerName = GameManager.GetPlayerName(); // Use the proper method
        string aiName = gameManager.GetRandomAIName(); // Use the instance properly

        // Show countdown
        vsText.text = playerName;
        yield return new WaitForSeconds(2f); // Adjust the time as needed
        vsText.text = "VS";
        yield return new WaitForSeconds(1.8f);
        vsText.text = aiName;
        yield return new WaitForSeconds(2.5f);

        vsText.gameObject.SetActive(false); // Hide countdown text
    }
	
	public void HidePlayerIntro()
    {
        vsText.gameObject.SetActive(false);
    }
}

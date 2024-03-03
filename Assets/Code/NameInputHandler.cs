using UnityEngine;
using TMPro;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField nameInputField; // Reference to the TMP InputField in the Inspector
	public TextMeshProUGUI titleText;

    private const string PlayerNameKey = "PlayerName"; // Key for PlayerPrefs

    private void Start()
    {
        // Load the saved name from PlayerPrefs and set it to the TMP input field
        string savedName = PlayerPrefs.GetString(PlayerNameKey);
        nameInputField.text = savedName;
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;

        // Save the player's name to PlayerPrefs
        PlayerPrefs.SetString(PlayerNameKey, playerName);
		
		titleText.fontSize = 63;
		titleText.text = "Player name saved!";

        // You can provide feedback to the player that their name was saved if needed
        Debug.Log("Player name saved: " + playerName);
		
		Invoke("ResetTitle", 3);
    }
	
	public void ResetTitle()
	{
		titleText.fontSize = 108;
		titleText.text = "settings";
	}
}

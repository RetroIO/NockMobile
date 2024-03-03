using UnityEngine;
using TMPro;

public class JumpOptionsHandler : MonoBehaviour
{
    public TMP_Dropdown jumpModeDropdown; // Reference to the TMP Dropdown component in the Inspector
	public TextMeshProUGUI titleText;
	
    private const string JumpModePrefKey = "JumpMode"; // Key for PlayerPrefs

    private void Start()
    {
        // Load the saved jump mode from PlayerPrefs and set it to the TMP Dropdown
        int savedJumpMode = PlayerPrefs.GetInt(JumpModePrefKey, 0); // Default to 0 (single jump)
        jumpModeDropdown.value = savedJumpMode;

        // Attach a callback to the TMP Dropdown's OnValueChanged event
        jumpModeDropdown.onValueChanged.AddListener(OnJumpModeDropdownChanged);
    }

    private void OnJumpModeDropdownChanged(int selectedOption)
    {
        // Save the selected jump mode to PlayerPrefs
        PlayerPrefs.SetInt(JumpModePrefKey, selectedOption);
		
		titleText.fontSize = 63;
		titleText.text = "Jump mode saved!";
		
		Debug.Log("Player name saved: " + selectedOption);
		Invoke("ResetTitle", 3);
    }
	
	public void ResetTitle()
	{
		titleText.fontSize = 108;
		titleText.text = "settings";
	}
}

using UnityEngine;
using TMPro;

public class MatchLengthHandler : MonoBehaviour
{
    public TMP_Dropdown matchLengthDropdown; // Reference to the TMP Dropdown component in the Inspector
    public TextMeshProUGUI titleText;

    private const string MatchLengthPrefKey = "MatchLength"; // Key for PlayerPrefs
    private float[] matchLengths = { 30f, 60f, 120f, 180f, 300f };

    private void Start()
    {
        // Load the previously saved match length from PlayerPrefs and set it in the dropdown
        int savedMatchLengthIndex = PlayerPrefs.GetInt(MatchLengthPrefKey, 2); // 0 is the default value if not set
        matchLengthDropdown.value = savedMatchLengthIndex;

        // Attach a callback to the TMP Dropdown's OnValueChanged event
        matchLengthDropdown.onValueChanged.AddListener(OnMatchLengthDropdownChanged);
    }


    private void OnMatchLengthDropdownChanged(int selectedMatchLengthIndex)
    {
        float selectedMatchLength = matchLengths[selectedMatchLengthIndex];
        PlayerPrefs.SetFloat("MatchLength", selectedMatchLength);
        PlayerPrefs.Save();

        titleText.fontSize = 63;
        titleText.text = "Match length saved!";

        Invoke("ResetTitle", 3);
    }


    public void ResetTitle()
    {
        titleText.fontSize = 108;
        titleText.text = "settings";
    }
}

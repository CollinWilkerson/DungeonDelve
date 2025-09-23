using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{

    private TextMeshProUGUI goldText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goldText = gameObject.GetComponent<TextMeshProUGUI>();
        if (!goldText)
        {
            Debug.LogError("No TextMeshPro on Gold Text gameObject");
        }

        //i know this seems stupid but it works better with the encounters
        PlayerData.AddTempGold(10);
        PlayerData.AddGold();
        //update display
        goldText.text = PlayerData.GetGold() + "G";
    }

    public void UpdateGoldText()
    {
        goldText.text = PlayerData.GetGold() + "G";
    }
}

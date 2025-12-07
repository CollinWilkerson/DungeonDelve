using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardDisplay : MonoBehaviour
{
    [SerializeField] private Image rewardDisplay;
    [SerializeField] private TextMeshProUGUI rewardText;

    public void DisplayReward(Equipment eq)
    {
        rewardDisplay.sprite = eq.GetSprite();
        rewardText.text = eq.GetName();
    }

    public void DisplayReward(int item)
    {
        rewardText.text = DataFiles.Items[item].Split(",")[0];
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PotionSeller : MonoBehaviour, IEvent
{
    private const int price = 20;
    public string GetButtonText_Option1()
    {
        if (PlayerData.GetGold() < price)
        {
            return "Not enough gold";
        }
        return "Heal Party - " + price + "G";
    }

    public string GetButtonText_Option2()
    {
        return "Rob the creature";
    }

    public string GetButtonText_Option3()
    {
        return "Pass on";
    }

    public string GetDescriptionText()
    {
        return "Your party comes across some torches in the otherwise dim cave. At the source of the light " +
            "they find a creature wrapped in cloth, masking its size. He offers to heal your party, " +
            "for a price.";
    }

    public Sprite GetEventImage()
    {
        throw new System.NotImplementedException();
    }

    public int GetValidOptions()
    {
        return 3;
    }

    public void Option1()
    {
        if (PlayerData.GetGold() < price)
        {
            return;
        }

        PlayerData.SpendGold(price);
        HealParty();
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }

    private static void HealParty()
    {
        foreach (MercObject merc in MercObject.Party)
        {
            merc?.UpdateHealth(merc.GetMaxHealth());
        }
    }

    public void Option2()
    {
        if(Random.Range(0, MercObject.GetTotalDamage()) > PlayerData.levelsCleared)
        {
            MercObject.ClearParty();
            SceneManager.LoadScene("EncounterLoss");
            return;
        }
        HealParty();
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }

    public void Option3()
    {
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }
}

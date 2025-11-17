using UnityEngine;
using UnityEngine.SceneManagement;

public class Fetch : MonoBehaviour, IEvent
{
    public const int price = 20;
    public string GetButtonText_Option1()
    {
        if (PlayerData.GetGold() < price)
        {
            return "Not enough gold";
        }
        return "Retrive Equipment - " + price + "G";
    }

    public string GetButtonText_Option2()
    {
        return "Pass on";
    }

    public string GetButtonText_Option3()
    {
        return "";
    }

    public string GetDescriptionText()
    {
        return "You aproach a pile of glittering gems, only for a creature to materialize on top of it." +
            "It says its been watching you, and can get back the equipment you've lost, for a price.";
    }

    public Sprite GetEventImage()
    {
        throw new System.NotImplementedException();
    }

    public int GetValidOptions()
    {
        return 2;
    }

    public void Option1()
    {
        if(PlayerData.GetGold() < price)
        {
            return;
        }
        for(int i = 0; i < PlayerData.level3Cutoff; i++)
        {
            LostEquipment.GetLostEquipment(i);
        }
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }

    public void Option2()
    {
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }

    public void Option3()
    {
        throw new System.NotImplementedException();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LostHero : MonoBehaviour, IEvent
{
    public string GetButtonText_Option1()
    {
        return "Leave them";
    }

    public string GetButtonText_Option2()
    {
        return "Recruit them";
    }

    public string GetButtonText_Option3()
    {
        return "NOT USED";
    }

    public string GetDescriptionText()
    {
        if (PartyHasOpenSlot())
        {
            return "You pass a worn down hero in the caverns, You can recruit them into your party, " +
                    "or you can point them in the direction of the exit and pass on.";
        }
        return "You pass a worn down hero in the caverns, but 5 heroes would attract too much attention. " +
            "You point them in the direction of the exit and pass on.";
    }

    public Sprite GetEventImage()
    {
        throw new System.NotImplementedException();
    }

    public int GetValidOptions()
    {
        if (PartyHasOpenSlot())
        {
            return 2;
        }
        return 1;
    }

    public void Option1()
    {
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }

    public void Option2()
    {
        MercObject.AddHeroToParty(new MercObject(Random.Range(0, DataFiles.Heroes.Length)));
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }

    public void Option3()
    {
        throw new System.NotImplementedException();
    }

    private bool PartyHasOpenSlot()
    {
        for (int i = 0; i < MercObject.PartySize; i++)
        {
            if (MercObject.Party[i] == null)
            {
                return true;
            }
        }
        return false;
    }
}

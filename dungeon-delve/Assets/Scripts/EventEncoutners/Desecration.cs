using UnityEngine;
using UnityEngine.SceneManagement;

public class Desecration : MonoBehaviour, IEvent
{
    public string GetButtonText_Option1()
    {
        return "Rob the graves";
    }

    public string GetButtonText_Option2()
    {
        return "Leave them be";
    }

    public string GetButtonText_Option3()
    {
        //possibly a better option if the player has enough int or something
        return "NOT USED";
    }

    public string GetDescriptionText()
    {
        return "As your party wanders the dugeon they pass some lavish graves. There might be some good" +
            " loot here, but there could also be evil ghosts.";
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
        if(Random.Range(0,2) == 0)
        {
            KillFirstPartyMember();
            return;
        }
        EncounterRewards.GetTreasure();
        EncounterRewards.GetTreasure();
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }

    private static void KillFirstPartyMember()
    {
        MercObject.DeletePartyMemeber(0);
        for (int i = 0; i < MercObject.Party.Length; i++)
        {
            if (MercObject.Party[i] != null)
            {
                MercObject.SwapPartyMembers(i, 0);
                PlayerData.levelsCleared++;
                SceneManager.LoadScene("EncounterWin");
                return;
            }
        }
        SceneManager.LoadScene("EncounterLoss");
        return;
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

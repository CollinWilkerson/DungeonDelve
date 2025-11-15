using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretPassage : MonoBehaviour, IEvent
{
    public string GetDescriptionText()
    {
        return "You find a tunnel in the side of the otherwise smooth dungeon walls, " +
            "It looks lightly travled and will let you swiftly decend into the dungeon.";
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
        PlayerData.levelsCleared++;
        SceneManager.LoadScene("EncounterWin");
    }

    public string GetButtonText_Option1()
    {
        return "Do not enter";
    }

    public void Option2()
    {
        PlayerData.levelsCleared = PlayerData.level1Cutoff;
        SceneManager.LoadScene("EncounterWin");
    }
    public string GetButtonText_Option2()
    {
        return "Go halfway";
    }

    public void Option3()
    {
        PlayerData.levelsCleared = PlayerData.level2Cutoff;
        SceneManager.LoadScene("EncounterWin");
    }
    public string GetButtonText_Option3()
    {
        return "Go to the end";
    }
}

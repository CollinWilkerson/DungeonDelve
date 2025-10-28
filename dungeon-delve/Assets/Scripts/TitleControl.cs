using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleControl : MonoBehaviour
{
    public void OnPlayButton() 
    {
        SceneManager.LoadScene("Tavern");
    }

    public void OnSettingsButton()
    {
        //openSettingsMenu
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnCodeButton()
    {
        Application.OpenURL("https://github.com/CollinWilkerson/DungeonDelve");
    }
}

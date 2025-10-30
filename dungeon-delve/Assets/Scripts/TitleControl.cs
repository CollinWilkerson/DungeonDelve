using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleControl : MonoBehaviour
{
    const string sourceCode = "https://github.com/CollinWilkerson/DungeonDelve";

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
        Application.OpenURL(sourceCode);
    }
}

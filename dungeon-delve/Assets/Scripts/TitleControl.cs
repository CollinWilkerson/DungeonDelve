using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleControl : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    private const string sourceCode = "https://github.com/CollinWilkerson/DungeonDelve";

    public void OnPlayButton() 
    {
        SceneManager.LoadScene("Tavern");
    }

    public void OnSettingsButton()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
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

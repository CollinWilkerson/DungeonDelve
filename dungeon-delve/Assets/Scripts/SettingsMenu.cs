using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public void CloseMenu(Image currentMenu)
    {
        currentMenu.gameObject.SetActive(false);
    }

    public void OpenMenu(Image menuToOpen)
    {
        menuToOpen.gameObject.SetActive(true);
    }

    public void ChangeLookSensitivity(Slider sensitivitySlider)
    {
        TavernData.MouseSensitivity = sensitivitySlider.value;
        //adjust sensitivity between platforms
        #if UNITY_EDITOR
            TavernData.MouseSensitivity *= 50;
        #endif
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

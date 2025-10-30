using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsInputs : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private InputAction pauseAction;

    private void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        if (pauseAction.triggered)
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
}

using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    private void Start()
    {
        if (TavernData.TutorialSeen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(gameObject);
        }
    }

    public void OnFinishTutorial()
    {
        Cursor.lockState = CursorLockMode.Locked;
        TavernData.TutorialSeen = true;
        Destroy(gameObject);
    }
}

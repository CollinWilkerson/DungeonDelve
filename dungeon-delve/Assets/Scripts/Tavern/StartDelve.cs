using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDelve : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetLayerMask();
    }

    public void Interact()
    {
        if(MercObject.Party[0] == null)
        {
            return;
        }
        Cursor.lockState = CursorLockMode.None;
        foreach(MercObject merc in MercObject.Party)
        {
            if(merc == null)
            {
                continue;
            }
            merc.UpdateHealth(merc.GetMaxHealth());
        }
        SceneManager.LoadScene(DataFiles.SelectEncounter());
    }

    private void SetLayerMask()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDelve : MonoBehaviour, IInteractable
{
    [SerializeField] private string[] EncounterScenes = { "MonsterEncounter" };

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
        SceneManager.LoadScene(EncounterScenes[Random.Range(0, EncounterScenes.Length)]);
    }

    private void SetLayerMask()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionMenu : MonoBehaviour
{
    [SerializeField] private string[] EncounterScenes = { "MonsterEncounter" };

    public void Advance()
    {
        //selects a random valid encounter scene and loads it, this may need to be adjusted for balance
        SceneManager.LoadScene(EncounterScenes[Random.Range(0, EncounterScenes.Length)]);
    }

    public void Retreat()
    {
        MercObject.ClearParty();
        //REPLACE WITH TAVERN SCENE
        SceneManager.LoadScene("MonsterEncounter");
    }
}

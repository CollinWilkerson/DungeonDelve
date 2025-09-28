using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TransitionMenu : MonoBehaviour
{
    [SerializeField] private string[] EncounterScenes = { "MonsterEncounter" };
    [SerializeField] private TextMeshProUGUI goldText;

    private void Start()
    {
        PlayerData.AddItem(Random.Range(1, 4));
        if (goldText)
        {
            StartCoroutine(MoveGold());
        }
    }

    public void Advance()
    {
        //selects a random valid encounter scene and loads it, this may need to be adjusted for balance
        SceneManager.LoadScene(EncounterScenes[Random.Range(0, EncounterScenes.Length)]);
    }

    public void Retreat()
    {
        SceneManager.LoadScene("Tavern");
    }

    private IEnumerator MoveGold()
    {
        
        int oldGold = PlayerData.GetGold();
        PlayerData.AddGold();
        int newGold = PlayerData.GetGold();
        goldText.text = oldGold + " + " + (newGold - oldGold) + " G";
        yield return new WaitForSeconds(1f);
        while (oldGold < newGold)
        {
            oldGold += 1;
            goldText.text = oldGold + " + " + (newGold - oldGold) + " G";
            yield return new WaitForSeconds(0.05f);
        }

        goldText.text = oldGold + " G";
    }
}

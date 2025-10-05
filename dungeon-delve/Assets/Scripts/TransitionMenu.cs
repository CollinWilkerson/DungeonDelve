using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TransitionMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    private void Start()
    {
        if (goldText)
        {
            StartCoroutine(MoveGold());
        }
    }

    public void Advance()
    {
        //selects a random valid encounter scene and loads it, this may need to be adjusted for balance
        SceneManager.LoadScene(DataFiles.SelectEncounter());
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

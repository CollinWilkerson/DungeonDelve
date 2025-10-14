using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TransitionMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI clearedText;

    [Header("Cards")]
    [SerializeField] private Transform CardGroup;
    [SerializeField] private GameObject nullCardPrefab;
    [SerializeField] private GameObject Lv1CardPrefab;
    [SerializeField] private GameObject Lv1BossPrefab;
    [SerializeField] private GameObject Lv2CardPrefab;
    [SerializeField] private GameObject Lv2BossPrefab;
    [SerializeField] private GameObject Lv3CardPrefab;
    [SerializeField] private GameObject FinalBossPrefab;


    private void Start()
    {
        if (goldText)
        {
            StartCoroutine(MoveGold());
        }

        if (clearedText)
        {
            clearedText.text = "Depth: " + (PlayerData.levelsCleared) + " + 1";
        }

        if (CardGroup)
        {
            for(int i = 0; i < 6; i++)
            {
                if (PlayerData.levelsCleared - 2 + i < 1)
                {
                    Instantiate(nullCardPrefab, CardGroup);
                    continue;
                }
                if (PlayerData.levelsCleared - 2 + i < PlayerData.level1Cutoff)
                {
                    Instantiate(Lv1CardPrefab, CardGroup);
                    continue;
                }
                if (PlayerData.levelsCleared - 2 + i == PlayerData.level1Cutoff)
                {
                    Instantiate(Lv1BossPrefab, CardGroup);
                    continue;
                }
                if (PlayerData.levelsCleared - 2 + i < PlayerData.level2Cutoff)
                {
                    Instantiate(Lv2CardPrefab, CardGroup);
                    continue;
                }
                if (PlayerData.levelsCleared - 2 + i == PlayerData.level2Cutoff)
                {
                    Instantiate(Lv2BossPrefab, CardGroup);
                    continue;
                }
                if (PlayerData.levelsCleared - 2 + i < PlayerData.level3Cutoff)
                {
                    Instantiate(Lv3CardPrefab, CardGroup);
                    continue;
                }
                if (PlayerData.levelsCleared - 2 + i == PlayerData.level3Cutoff)
                {
                    Instantiate(FinalBossPrefab, CardGroup);
                    continue;
                }
            }
            StartCoroutine(MoveCards());
        }
        //Debug.Log("levels cleared: " + PlayerData.levelsCleared);
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

    private IEnumerator MoveCards()
    {
        Vector3 endPos = CardGroup.localPosition - Vector3.right * CardGroup.localPosition.x * 2;

        yield return new WaitForSeconds(1f);
        while (CardGroup.localPosition.x > endPos.x + 3)
        {
            CardGroup.localPosition = Vector3.Lerp(CardGroup.localPosition, endPos, 10 * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }
        Debug.Log("finish");
        clearedText.text = "Depth: " + (PlayerData.levelsCleared + 1);
        CardGroup.localPosition = endPos;
    }
}

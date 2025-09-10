using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MonsterEncounter : MonoBehaviour
{
    [SerializeField] private float TickSpeed;

    private static MercenaryController[] HeroMercs;
    private static MercenaryController[] EnemyMercs;


    private void Start()
    {
        HeroMercs = new MercenaryController[4];
        EnemyMercs = new MercenaryController[4];

        foreach (MercenaryController mercenary in FindObjectsByType<MercenaryController>(FindObjectsSortMode.None))
        {
            if (mercenary.isHero)
            {
                Debug.Log("FoundHero");
                HeroMercs[mercenary.partyOrder] = mercenary;
            }
            else
            {
                Debug.Log("FoundEnemy");
                EnemyMercs[mercenary.partyOrder] = mercenary;
            }
        }

        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            Debug.Log("Tick");
            foreach (MercenaryController controller in HeroMercs)
            {
                if (controller)
                    controller.Tick();
            }
            foreach (MercenaryController controller in EnemyMercs)
            {
                if (controller)
                    controller.Tick();
            }
            yield return new WaitForSeconds(TickSpeed);
        }
    }

    public static MercenaryController GetEnemyTarget()
    {
        foreach(MercenaryController controller in EnemyMercs)
        {
            if (controller)
                return controller;
        }
        return null;
    }

    public static MercenaryController GetHeroTarget()
    {
        foreach (MercenaryController controller in HeroMercs)
        {
            if (controller)
                return controller;
        }
        return null;
    }

    public static void QuereyWin(MercenaryController deadMonster)
    {
        EnemyMercs[deadMonster.partyOrder] = null;
        foreach (MercenaryController mercenary in EnemyMercs)
        {
            if (mercenary)
                return;
        }
        Debug.Log("Hero Win");
        SceneManager.LoadScene("EncounterWin");
    }

    public static void QuereyLoss(MercenaryController deadHero)
    {
        HeroMercs[deadHero.partyOrder] = null;
        foreach (MercenaryController mercenary in HeroMercs)
        {
            if (mercenary)
                return;
        }
        Debug.Log("Monster Win");
        SceneManager.LoadScene("EncounterLoss");
    }
}

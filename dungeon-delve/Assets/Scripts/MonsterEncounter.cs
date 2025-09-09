using System.Collections;
using Unity.VisualScripting;
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
                HeroMercs[mercenary.partyOrder] = mercenary;
            }
            else
            {
                EnemyMercs[mercenary.partyOrder] = mercenary;
            }
        }

        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        foreach (MercenaryController controller in HeroMercs)
        {
            controller.Tick();
        }
        foreach (MercenaryController controller in EnemyMercs)
        {
            controller.Tick();
        }
        yield return new WaitForSeconds(TickSpeed);
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
}

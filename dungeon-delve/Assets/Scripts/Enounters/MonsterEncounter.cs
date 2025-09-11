using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MonsterEncounter : MonoBehaviour
{
    [SerializeField] private float TickSpeed;
    [SerializeField] private Transform monsterFrontline;
    [SerializeField] private Transform heroFrontline;

    private static MercenaryController[] HeroMercs;
    private static MercenaryController[] EnemyMercs;


    private void Start()
    {
        //THESE **MUST** BE AT THE TOP OF START
        HeroMercs = new MercenaryController[4];
        EnemyMercs = new MercenaryController[4];

        if(MercObject.Party[0] == null)
        {
            Debug.Log("no hero in party, Drafting human");
            MercObject.AddHeroToParty(new MercObject("HumanHeroes/HumanWarrior"));
        }

        SpawnHero();
        SpawnMonster();

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

    private void SpawnHero()
    {
        MercenaryController tempHero = Instantiate(Resources.Load<GameObject>(
            MercObject.Party[0].filePath),heroFrontline).GetComponentInChildren<MercenaryController>();
        HeroMercs[0] = tempHero;
        tempHero.InitailizeHero();

    }

    //spawns one level 1 monster
    private void SpawnMonster()
    {
        //this may be inneficient for large numbers of monsters
        GameObject[] monsters = Resources.LoadAll<GameObject>("MonstersLv1");
        //Debug.Log(monsters.Length);
        GameObject monster = Instantiate(monsters[
            Random.Range(0, monsters.Length)], monsterFrontline);
        if(EnemyMercs[0] = monster.GetComponentInChildren<MercenaryController>())
        {
            Debug.Log(monster.name + " spawned properly");
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
        MercObject.Party[0].UpdateHealth(HeroMercs[0].health);
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

    public void fastForward()
    {
        TickSpeed = TickSpeed / 2;
    }

    public void skipFight()
    {
        TickSpeed = 0f;
    }
}

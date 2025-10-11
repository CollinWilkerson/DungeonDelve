using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


[DefaultExecutionOrder(-9999)]
public class MonsterEncounter : MonoBehaviour
{
    [SerializeField] private float TickSpeed;
    private float StartTickSpeed;
    private bool run = true;
    [SerializeField] private Transform monsterFrontline;
    [SerializeField] private Transform heroFrontline;
    [SerializeField] private Transform heroBackline;

    private static HeroController[] HeroMercs;
    private static MonsterController[] EnemyMercs;

    public delegate void mercTick();
    public static event mercTick OnMercTick;

    private static List<Equipment> deadEq = new List<Equipment>();


    private void Awake()
    {
        //THESE **MUST** BE AT THE TOP OF START
        HeroMercs = new HeroController[4];
        EnemyMercs = new MonsterController[4];
        StartTickSpeed = TickSpeed;

        //for debuging, spawns a hero in an empty party
        if (MercObject.Party[0] == null)
        {
            Debug.Log("no hero in party, Drafting human");
            MercObject.AddHeroToParty(new MercObject("HumanHeroes/HumanWarrior", 0));
        }

        SpawnHero();
        SpawnMonster();
    }

    private void Start()
    {
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        while (run)
        {
            //Debug.Log("Tick");
            OnMercTick();
            yield return new WaitForSeconds(TickSpeed);
        }
    }


    private void SpawnHero()
    {
        MercObject hero = MercObject.Party[0];
        //instantiates a ahero from a resorces location, has a set filepath
        HeroController tempHero = Instantiate(Resources.Load<GameObject>(
            hero.filePath),heroFrontline).GetComponentInChildren<HeroController>();
        HeroMercs[0] = tempHero;
        tempHero.InitailizeHero(0, hero.armor, hero.weapon);

        
        for(int i = 1; i < MercObject.Party.Length; i++)
        {
            if (MercObject.Party[i] != null)
            {
                hero = MercObject.Party[i];
                tempHero = Instantiate(Resources.Load<GameObject>(
                    hero.filePath), heroBackline).GetComponentInChildren<HeroController>();
                HeroMercs[i] = tempHero;
                tempHero.InitailizeHero(i, hero.armor, hero.weapon);
            }
        }
    }

    //spawns one level 1 monster
    private void SpawnMonster()
    {
        //this may be inneficient for large numbers of monsters
        GameObject[] monsters = Resources.LoadAll<GameObject>("MonstersLv1");
        //Debug.Log(monsters.Length);
        GameObject monster = Instantiate(monsters[
            Random.Range(0, monsters.Length)], monsterFrontline);
        if(EnemyMercs[0] = monster.GetComponentInChildren<MonsterController>())
        {
            //Debug.Log(monster.name + " spawned properly");
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
        //Debug.Log("Hero Win");
        
        //updates the health for every existing merc
        for (int i = 0; i < MercObject.Party.Length; i++)
        {
            if (HeroMercs[i])
            {
                MercObject.Party[i].UpdateHealth(HeroMercs[i].health);
            }
        }

        //guarentees a hero in the first slot to prevent nullreference (this is gross and I hate it)
        if (!HeroMercs[0])
        {
            for (int i = 1; i < HeroMercs.Length; i++)
            {
                if (HeroMercs[i])
                {
                    HeroMercs[0] = HeroMercs[i];
                    HeroMercs[i] = null;
                    MercObject.SwapPartyMembers(0, i);
                }
            }
        }
        Equipment.AddEq(deadEq.ToArray());
        Equipment[] e = LostEquipment.GetLostEquipment(PlayerData.levelsCleared);
        if (e != null)
        {
            Debug.Log("regained eq");
        }
        Equipment.AddEq(e);
        //Equipment.AddEq(LostEquipment.GetLostEquipment(PlayerData.levelsCleared));
        PlayerData.levelsCleared += 1;
        SceneManager.LoadScene("EncounterWin");
    }

    public static void QuereyLoss(MercenaryController deadHero)
    {
        MercObject merc = MercObject.Party[deadHero.partyOrder];
        if (merc.armor != null)
        {
            deadEq.Add(merc.armor);
        }
        if (merc.weapon != null)
        {
            deadEq.Add(merc.weapon);
        }
        MercObject.DeletePartyMemeber(deadHero.partyOrder);
        HeroMercs[deadHero.partyOrder] = null;
        foreach (MercenaryController mercenary in HeroMercs)
        {
            if (mercenary)
            {
                //Debug.Log(mercenary.name + " is still alive");
                return;
            }
        }
        Debug.Log("Monster Win");
        LostEquipment.Insert(PlayerData.levelsCleared, deadEq.ToArray());
        SceneManager.LoadScene("EncounterLoss");
    }

    public void fastForward()
    {
        if (TickSpeed == StartTickSpeed)
        {
            TickSpeed = StartTickSpeed / 2;
            return;
        }
        TickSpeed = StartTickSpeed;

    }

    public void skipFight()
    {
        TickSpeed = 0f;
        /*foreach(MonsterController monster in EnemyMercs)
        {
            if(monster == null)
            {
                continue;
            }
            monster.skip = true;
        }
        foreach(HeroController hero in HeroMercs)
        {
            if(hero == null)
            {
                continue;
            }
            hero.skip = true;
        }
        */
    }

    public void stop()
    {
        run = !run;
        if (run)
        {
            StartCoroutine(Tick());
        }
    }

    public HeroController[] GetHeroes()
    {
        return HeroMercs;
    }
}

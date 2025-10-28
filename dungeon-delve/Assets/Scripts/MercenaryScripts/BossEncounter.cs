using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


[DefaultExecutionOrder(-9999)]
public class BossEncounter : MonsterEncounter
{
    [SerializeField] private Transform monsterBackline;


    private void Awake()
    {
        Debug.Log("AwakeBoss");
        //for debuging, spawns a hero in an empty party
        if (MercObject.Party[0] == null)
        {
            Debug.Log("no hero in party, Drafting human");
            MercObject.AddHeroToParty(new MercObject("HumanHeroes/HumanWarrior", 0));
        }

        SpawnHero();
        SpawnBoss(DataFiles.SelectBoss());
        //SpawnMinions(DataFiles.SelectMinon(), 2);
    }

    private void Start()
    {
        StartCoroutine(Tick());
    }

    //spawns one level 1 monster
    private void SpawnMinions(GameObject minion, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject monster = Instantiate(minion, monsterBackline);
            EnemyMercs[1 + i] = monster.GetComponentInChildren<MonsterController>();
        }
    }

    private void SpawnBoss(GameObject boss)
    {
        GameObject monster = Instantiate(boss, monsterFrontline);
        EnemyMercs[0] = monster.GetComponentInChildren<MonsterController>();
    }
}

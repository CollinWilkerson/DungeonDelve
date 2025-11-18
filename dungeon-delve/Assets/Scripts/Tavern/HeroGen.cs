using UnityEngine;
using System.IO;

public class HeroGen : MonoBehaviour
{
    private const string HumanHeroFilepath = "HumanHeroes/Tavern";
    private const string LegendaryHeroFilepath = "Legends/Tavern";

    [SerializeField] GameObject[] TableObjects;
    [SerializeField] GameObject Shopkeep;

    private GameObject[] spawnPoints;
    private int heroesToSpawn;

    private void Start()
    {
        MakeTablesActive();
        TrySpawnShopkeep();
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        heroesToSpawn = spawnPoints.Length;

        SpawnHeroes();

    }

    public void TrySpawnShopkeep()
    {
            Shopkeep.SetActive(TavernData.shopPurchased);
    }

    private void SpawnHeroes()
    {
        //spawns all surving party members
        SpawnSurvivingHeroes();
        // Idea is to spawn remaining heroes, spawn 3 normal heroes 1 legend, then random the rest.

        GameObject[] humanHeroPool = Resources.LoadAll<GameObject>(HumanHeroFilepath);
        for (int i = 0; i < 3; i++)
        {
            if (heroesToSpawn == 0)
            {
                return;
            }
            Instantiate(humanHeroPool[Random.Range(0, humanHeroPool.Length)],
                spawnPoints[heroesToSpawn - 1].transform);
            heroesToSpawn--;
        }
        if (heroesToSpawn == 0)
        {
            return;
        }
        GameObject[] LegendHeroPool = Resources.LoadAll<GameObject>(LegendaryHeroFilepath);
        Instantiate(LegendHeroPool[Random.Range(0, LegendHeroPool.Length)],
            spawnPoints[heroesToSpawn - 1].transform);
        heroesToSpawn--;
        while (heroesToSpawn > 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                Instantiate(LegendHeroPool[Random.Range(0, LegendHeroPool.Length)],
                    spawnPoints[heroesToSpawn - 1].transform);
                heroesToSpawn--;
                continue;
            }
            Instantiate(humanHeroPool[Random.Range(0, humanHeroPool.Length)],
                spawnPoints[heroesToSpawn - 1].transform);
            heroesToSpawn--;
        }
    }

    private int SpawnSurvivingHeroes()
    {
        foreach (MercObject merc in MercObject.Party)
        {
            if (merc != null)
            {
                GameObject hero = Instantiate(GenerateHeroByIndex(merc.index),
                    spawnPoints[heroesToSpawn - 1].transform);
                if (merc.armor != null)
                {
                    Equipment.AddEq(merc.armor);
                }
                if (merc.weapon != null)
                {
                    Equipment.AddEq(merc.weapon);
                }
                hero.GetComponent<HeroInteraction>().DiscountSurvivingHero(0.5f);//this feels exceptionally dumb but i have deadlines
                hero.GetComponent<HeroInteraction>().SetName(merc.GetName());
                heroesToSpawn--;
            }
        }

        MercObject.ClearParty();
        return heroesToSpawn;
    }

    private void MakeTablesActive()
    {
        for (int i = 0; i < TavernData.tables; i++)
        {
            if (i > TableObjects.Length)
            {
                break;
            }
            TableObjects[i].SetActive(true);
        }
    }

    private GameObject GenerateHeroByIndex(int index)
    {
        string filepath = DataFiles.Heroes[index].Split(',')[8];

        return Resources.Load<GameObject>(filepath);
    }

    public void SetTableActive(int TableToActivate)
    {
        TableObjects[TableToActivate].SetActive(true);
    }
}

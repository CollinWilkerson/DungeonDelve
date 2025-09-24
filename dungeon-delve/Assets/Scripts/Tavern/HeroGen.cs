using UnityEngine;
using System.IO;

public class HeroGen : MonoBehaviour
{
    [SerializeField] private int humansToGenerate = 1;

    private GameObject[] spawnPoints;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int spawned = 0;

        //spawns all surving party members
        foreach (MercObject merc in MercObject.Party)
        {
            if (merc != null)
            {
                GameObject hero = Instantiate(GenerateHeroByIndex(merc.index), spawnPoints[spawned % spawnPoints.Length].transform);
                hero.GetComponent<HeroInteraction>().DiscountSurvivingHero();//this feels exceptionally dumb but i have deadlines
                spawned++;
            }
        }

        MercObject.ClearParty();

        //spawns new party members to fill the gap
        GameObject[] hiringPool = GenerateHero("HumanHeroes/Tavern", humansToGenerate - spawned);
                        
        foreach (GameObject go in hiringPool)
        {
            Instantiate(go, spawnPoints[spawned%spawnPoints.Length].transform);
            spawned++;
        }
    }

    private GameObject GenerateHeroByIndex(int index)
    {
        string filepath = File.ReadAllLines(
                "Assets/Resources/Data/heroStats.csv")[index].Split(',')[8];

        return Resources.Load<GameObject>(filepath);
    }

    /// <summary>
    /// Generates 1 hero from a selected filepath
    /// </summary>
    /// <param name="filepath">the filepath to generate the hero from "Resources/(filepath)"</param>
    private GameObject GenerateHero(string filepath)
    {
        GameObject[] heroPool = Resources.LoadAll<GameObject>(filepath);
        return heroPool[Random.Range(0 , heroPool.Length)];
    }

    /// <summary>
    /// Generates a selected amount of heroes from a selected filepath
    /// </summary>
    /// <param name="filepath">the filepath to generate the hero from "Resources/(filepath)"</param>
    /// <param name="amount">amount of heroes to generate</param>
    /// <returns></returns>
    private GameObject[] GenerateHero(string filepath, int amount)
    {
        GameObject[] heroPool = Resources.LoadAll<GameObject>(filepath);
        GameObject[] returnHeroes = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            returnHeroes[i] = heroPool[Random.Range(0, heroPool.Length)];
        }
        return returnHeroes;
    }
}

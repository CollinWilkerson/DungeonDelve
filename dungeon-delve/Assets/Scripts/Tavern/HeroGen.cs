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
                GameObject hero = Instantiate(GenerateHeroByIndex(merc.index), 
                    spawnPoints[spawned % spawnPoints.Length].transform);
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
        string filepath = DataFiles.Heroes[index].Split(',')[8];

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

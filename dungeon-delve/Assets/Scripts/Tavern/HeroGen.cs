using UnityEngine;

public class HeroGen : MonoBehaviour
{
    [SerializeField] private int humansToGenerate = 1;

    private GameObject[] spawnPoints;

    private void Start()
    {
       GameObject[] hiringPool = GenerateHero("HumanHeroes/Tavern", humansToGenerate);

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        int spawned = 0;
        
        foreach (GameObject go in hiringPool)
        {
            Instantiate(go, spawnPoints[spawned%spawnPoints.Length].transform);
            spawned++;
        }
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

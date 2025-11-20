using NUnit.Framework;
using UnityEngine;

public class HeroGenTests
{
    GameObject Shopkeeper;
    GameObject[] tables = new GameObject[4];
    GameObject[] spawnpoints = new GameObject[4];
    HeroGen heroGen;

    [SetUp]
    public void SetUp()
    {
        Shopkeeper = new GameObject("Shopkeeper");
        Shopkeeper.SetActive(false);
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i] = new GameObject("table" + i);
            spawnpoints[i] = new GameObject("spawn" + i);
            spawnpoints[i].tag = "SpawnPoint";
        }

        heroGen = new GameObject("GameManager").AddComponent<HeroGen>();
        heroGen.TestSetup(tables, Shopkeeper);
    }

    [Test]
    public void ShopKeepSetActiveWhenOwned()
    {
        TavernData.shopPurchased = true;
        heroGen.TrySpawnShopkeep();
        Assert.IsTrue(Shopkeeper.activeSelf);
    }

    [Test]
    public void ShopkeepSetInactiveWhenNotOwned()
    {
        TavernData.shopPurchased = false;
        heroGen.TrySpawnShopkeep();
        Assert.IsFalse(Shopkeeper.activeSelf);
    }

    [Test]
    public void SetTableActiveSetsTableActive()
    {
        foreach (GameObject go in tables)
        {
            go.SetActive(false );
        }

        for (int i = 0; i < tables.Length; i++)
        {
            heroGen.SetTableActive(i);
            Assert.IsTrue(tables[i].activeSelf);
        }
    }
}

using UnityEngine;
using System;
using System.IO;

public class MonsterController : MercenaryController
{
    private int goldValue;
    private int itemsToGive;

    private void Awake()
    {
        //set up healthbar
        statDisplay = GetComponentInChildren<StatDisplay>();
        //value setup
        string[] values = File.ReadAllLines(
            "Assets/Resources/Data/monsterStats.csv")[index].Split(',');
        maxHealth = Int32.Parse(values[1]);
        damage = Int32.Parse(values[2]);
        speed = Int32.Parse(values[3]);
        goldValue = Int32.Parse(values[4]);
        itemsToGive = Int32.Parse(values[5]);
        health = maxHealth;

        statDisplay.SetHealthbar(maxHealth);

        MonsterEncounter.OnMercTick += Tick;
    }

    private void OnDisable()
    {
        MonsterEncounter.OnMercTick -= Tick;
    }

    public override void Tick()
    {
        time += speed;
        if (statDisplay)
            statDisplay.UpdateSpeedBar(time);
        if (time > 100)
        {
            attackControl.Attack(MonsterEncounter.GetHeroTarget(), damage);
            time = 0;
        }
    }

    public override void TakeDamage(int damage)
    {
        //Debug.Log(gameObject.name + " takes " +  damage + " damage");
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        statDisplay.UpdateHealthbar(health);
        if (health <= 0)
        {
            for (int i = 0; i < itemsToGive; i++)
            {
                PlayerData.AddItem(UnityEngine.Random.Range(1, 4));
            }

            PlayerData.AddTempGold(goldValue);
            MonsterEncounter.QuereyWin(this);
        }
    }
}

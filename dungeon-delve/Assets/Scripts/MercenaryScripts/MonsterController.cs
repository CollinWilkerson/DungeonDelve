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
        string[] values = DataFiles.Monsters[index].Split(',');
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
            // This would probably do better in a result screen but for now this will work
            EncounterRewards.GetTreasure();

            PlayerData.AddTempGold(goldValue);
            MonsterEncounter.QuereyWin(this);
        }
    }
}

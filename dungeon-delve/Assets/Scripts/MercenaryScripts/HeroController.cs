using UnityEngine;
using System;
using System.IO;

public class HeroController : MercenaryController
{
    private void Awake()
    {
        //set up healthbar
        statDisplay = GetComponentInChildren<StatDisplay>();
        //value setup
        string[] values = DataFiles.Heroes[index].Split(',');
        maxHealth = Int32.Parse(values[1]);
        damage = Int32.Parse(values[2]);
        speed = Int32.Parse(values[3]);

        MonsterEncounter.OnMercTick += Tick;
    }

    private void OnDisable()
    {
        MonsterEncounter.OnMercTick -= Tick;
    }

    private void Start()
    {
        //ensures the character is using the propper attackControl, creates a default if there is none
        if (gameObject.GetComponent<IAttack>() == null)
        {
            attackControl = gameObject.AddComponent<DefaultAttack>();
            Debug.LogWarning("No attack component on " + gameObject.name + ", Using default");
        }
        else
        {
            attackControl = gameObject.GetComponent<IAttack>();
        }

        //ensures the chatacter is using the propper defenceControl, creates a default if there is none
        if (gameObject.GetComponent<IDefend>() == null)
        {
            defenceControl = gameObject.AddComponent<DefaultDefend>();
            Debug.LogWarning("No defence component on " + gameObject.name + ", Using default");
        }
        else
        {
            defenceControl = gameObject.GetComponent<IDefend>();
        }


        //sets up the defence so that it can give damage to the player
        defenceControl.Initialize(this);

        FindAnyObjectByType<HeroButtonContainer>(FindObjectsInactive.Include).RegisterHero(this);
    }

    /// <summary>
    /// copies the heroes health from the object and sets up healthbars
    /// </summary>
    public void InitailizeHero(int _partyOrder, Equipment armor, Equipment weapon )
    {
        if(armor != null)
        {
            maxHealth += armor.GetHealth();
            damage += armor.GetDamage();
            speed += armor.GetSpeed();
        }
        if (weapon != null)
        {
            maxHealth += weapon.GetHealth();
            damage += weapon.GetDamage();
            speed += weapon.GetSpeed();
        }

        if(maxHealth < 1)
        {
            maxHealth = 1;
        }

        statDisplay.SetHealthbar(maxHealth);

        //i could probably just do the stat adjustments here
        health = MercObject.Party[_partyOrder].GetHealth();
        if (health == -99)
        {
            Heal(int.MaxValue);
            return;
        }
        statDisplay.UpdateHealthbar(health);
        partyOrder = _partyOrder;
    }

    public override void Tick()
    {
        time += speed;
        if (statDisplay)
            statDisplay.UpdateSpeedBar(time);
        if (time > 100) //if the hero has hit their attack time, find a target and hit them
        {
            attackControl.Attack(MonsterEncounter.GetEnemyTarget(), damage);
            time = 0;
        }
    }

    /// <summary>
    /// Damages the player and handles their death, only to be called by defenceControl
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage)
    {
        //Debug.Log(gameObject.name + " takes " +  damage + " damage");
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        statDisplay.UpdateHealthbar(health);
        if (health <= 0)
        {
                MonsterEncounter.QuereyLoss(this);
        }
    }
}

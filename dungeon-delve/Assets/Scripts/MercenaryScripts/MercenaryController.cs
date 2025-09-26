using UnityEngine;
using System;
using System.IO;

public class MercenaryController : MonoBehaviour
{
    public IDefend defenceControl;
    public bool isHero = true;
    [HideInInspector] public int partyOrder = 0;

    [SerializeField] private int index = 1;
    private int maxHealth = 10;
    [HideInInspector] public int health;
    private int damage = 1;
    private int speed = 1;
    private int goldValue = 1;

    private int time = 0;
    private StatDisplay statDisplay;
    private IAttack attackControl;

    private void Awake()
    {
        //set up healthbar
        statDisplay = GetComponentInChildren<StatDisplay>();
        if (!isHero) //setup monster from different csv
        {
            string[] values = File.ReadAllLines(
                "Assets/Resources/Data/monsterStats.csv")[index].Split(',');
            maxHealth = Int32.Parse(values[1]);
            damage = Int32.Parse(values[2]);
            speed = Int32.Parse(values[3]);
            goldValue = Int32.Parse(values[4]);
            health = maxHealth;
        }
        else //setup hero from csv
        {
            string[] values = File.ReadAllLines(
                "Assets/Resources/Data/heroStats.csv")[index].Split(',');
            maxHealth = Int32.Parse(values[1]);
            damage = Int32.Parse(values[2]);
            speed = Int32.Parse(values[3]);
        }
        statDisplay.SetHealthbar(maxHealth);
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
    }
    public void Tick()
    {
        time += speed;
        if(statDisplay)
            statDisplay.UpdateSpeedBar(time);
        if (time > 100)
        {
            MercenaryController target;
            //Debug.Log(gameObject.name + " Attacks for " + damage);
            if (isHero)
            {
                target = MonsterEncounter.GetEnemyTarget();

            }
            else
            {
                target = MonsterEncounter.GetHeroTarget();
            }
            attackControl.Attack(target, damage);
            time = 0;
        }
    }

    /// <summary>
    /// Damages the player and handles their death, only to be called by defenceControl
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        //Debug.Log(gameObject.name + " takes " +  damage + " damage");
        health = Mathf.Clamp(health-damage, 0, maxHealth);
        statDisplay.UpdateHealthbar(health);
        if (health <= 0)
        {
            //Debug.Log(gameObject.name + " has died");
            if (isHero)
            {
                MonsterEncounter.QuereyLoss(this);
            }
            else
            {
                PlayerData.AddTempGold(goldValue);
                MonsterEncounter.QuereyWin(this);
            }
        }
    }

    /// <summary>
    /// copies the heroes health from the object and sets up healthbars
    /// </summary>
    public void InitailizeHero(int _partyOrder)
    {
        health = MercObject.Party[_partyOrder].GetHealth();
        if (health == -99)
        {
            Heal(int.MaxValue);
            return;
        }
        statDisplay.UpdateHealthbar(health);
        partyOrder = _partyOrder;
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        statDisplay.UpdateHealthbar(health);
    }

    public void DamageBoost(int amount)
    {
        damage += amount;
    }
}

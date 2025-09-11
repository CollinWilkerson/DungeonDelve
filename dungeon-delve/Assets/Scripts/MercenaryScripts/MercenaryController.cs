using UnityEngine;

public class MercenaryController : MonoBehaviour
{
    public IDefend defenceControl;
    public bool isHero = true;
    public int partyOrder = 0;

    [SerializeField] private int maxHealth = 10;
    public int health;
    [SerializeField] private int damage = 1;
    [SerializeField] private int speed = 1;
    private int time = 0;
    private StatDisplay statDisplay;
    private IAttack attackControl;

    private void Awake()
    {
        //set up healthbar
        statDisplay = GetComponentInChildren<StatDisplay>();
        if (!isHero)
        {
            health = maxHealth;
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
            Debug.Log(gameObject.name + " Attacks for " + damage);
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
        Debug.Log(gameObject.name + " takes " +  damage + " damage");
        health = Mathf.Clamp(health-damage, 0, maxHealth);
        statDisplay.UpdateHealthbar(health);
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " has died");
            if (isHero)
            {
                MonsterEncounter.QuereyLoss(this);
            }
            else
            {
                MonsterEncounter.QuereyWin(this);
            }
        }
    }

    public void InitailizeHero()
    {
        health = MercObject.Party[0].GetHealth();
        if (health == -99)
        {
            Heal(int.MaxValue);
            return;
        }
        statDisplay.UpdateHealthbar(health);
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        statDisplay.UpdateHealthbar(health);
    }
}

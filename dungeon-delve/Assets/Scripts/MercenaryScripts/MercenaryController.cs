using UnityEngine;
using TMPro;

public class MercenaryController : MonoBehaviour
{
    public IDefend defenceControl;
    public bool isHero = true;
    public int partyOrder = 0;

    [SerializeField] private int maxHealth = 10;
    private int health;
    [SerializeField] private int damage = 1;
    [SerializeField] private int speed = 1;
    [SerializeField] private TextMeshProUGUI healthBar;
    private int time = 0;
    private IAttack attackControl;


    private void Start()
    {
        //RANDOMS FOR SHOWCASE PURPOUSES
        maxHealth = Random.Range(10, 20);
        speed = Random.Range(1, 3);
        damage = Random.Range(1, 3);

        //THIS WILL CAUSE PROBLEMS WITH PERSISTANT HEALTH LATER
        health = maxHealth;
        UpdateHealthbar();

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
        time++;
        if (time > 100 / speed)
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
        health -= damage;
        UpdateHealthbar();
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

    private void UpdateHealthbar()
    {
        healthBar.text = health + "/" + maxHealth;
    }
}

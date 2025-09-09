using UnityEngine;

public class MercenaryController : MonoBehaviour
{
    public IDefend defenceControl;
    public bool isHero = true;
    public int partyOrder = 0;

    private int health = 10;
    private int damage = 1;
    private int speed = 1;
    private int time = 0;
    private IAttack attackControl;


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
        time++;
        if (time > 1000 / speed)
        {
            Debug.Log(gameObject.name + " Attacks for " + damage);
            if (isHero)
            {
                attackControl.Attack(MonsterEncounter.GetEnemyTarget(), damage);
            }
            else
            {
                attackControl.Attack(MonsterEncounter.GetHeroTarget(), damage);
            }
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
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " has died");
        }
    }
}

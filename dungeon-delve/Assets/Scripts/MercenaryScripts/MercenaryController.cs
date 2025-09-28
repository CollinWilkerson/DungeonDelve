using UnityEngine;
using System;
using System.IO;

public class MercenaryController : MonoBehaviour
{
    public IDefend defenceControl;
    [HideInInspector] public int partyOrder = 0;

    [SerializeField] protected int index = 1;
    protected int maxHealth = 10;
    [HideInInspector] public int health;
    protected int damage = 1;
    protected int speed = 1;

    protected int time = 0;
    protected StatDisplay statDisplay;
    protected IAttack attackControl;

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
    public virtual void Tick()
    {
        Debug.LogError("MercenaryController is not to be used");
    }

    /// <summary>
    /// Damages the player and handles their death, only to be called by defenceControl
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
        Debug.LogError("MercenaryController is not to be used");
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

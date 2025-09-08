using UnityEngine;

public class MercenaryController : MonoBehaviour
{
    private int health;
    private int damage;
    private int speed;
    private IAttack attackControl;
    public IDefend defenceControl;

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

    /// <summary>
    /// Damages the player and handles their death, only to be called by defenceControl
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " has died");
        }
    }
}

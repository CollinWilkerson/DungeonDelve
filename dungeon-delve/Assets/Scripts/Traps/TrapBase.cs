using UnityEngine;

public class TrapBase : MonoBehaviour
{
    [SerializeField] protected float time = 5f;
    
    protected bool end = false;
    protected int heroes = 1;

    public static TrapBase activeTrap;
    public int goldValue = 2;
    public int damage = 0;

    //not really sure how to implement this without touching everything so im leaving it out rn
    public virtual void TrapLossEffects()
    {
        return;
    }

    protected void Pass()
    {
        if (end)
        {
            return;
        }
        FindAnyObjectByType<TrapResult>().WinTrap(this);
        end = true;
    }

    protected void Fail()
    {
        //add the health loss and stuff
        if (end)
        {
            return;
        }
        FindAnyObjectByType<TrapResult>().LoseTrap(this);
        end = true;
    }

    protected void GetHeroes(Job job)
    {
        if (MercObject.Party != null)
        {
            switch (job)
            {
                case Job.warrior:
                    foreach (MercObject merc in MercObject.Party)
                    {
                        if (merc != null)
                        {
                            merc.GetWarrior();
                        }
                    }
                    return;
                case Job.ranger:
                    foreach (MercObject merc in MercObject.Party)
                    {
                        if (merc != null)
                        {
                            merc.GetRanger();
                        }
                    }
                    return;
                default:
                    foreach (MercObject merc in MercObject.Party)
                    {
                        if (merc != null)
                        {
                            merc.GetMage();
                        }
                    }
                    return;
            }
        }
    }
}

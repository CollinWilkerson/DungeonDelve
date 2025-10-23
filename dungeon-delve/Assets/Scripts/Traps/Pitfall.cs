using UnityEngine;
using System.Collections;

public class Pitfall : TrapBase
{
    [SerializeField] private float timeToLose = 5f;

    private bool end = false;
    private int rangers;

    void Start()
    {
        if (MercObject.Party != null)
        {
            foreach (MercObject merc in MercObject.Party)
            {
                if (merc != null)
                {
                    rangers += merc.GetRanger();
                }
            }
        }

        if (rangers > 5)
        {
            Pass();
            return;
        }

        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToLose);

        Fail();
    }

    public void Pass()
    {
        if (end)
        {
            return;
        }
        FindAnyObjectByType<TrapResult>().WinTrap(this);
        end = true;
    }

    private void Fail()
    {
        //add the health loss and stuff
        if (end)
        {
            return;
        }
        FindAnyObjectByType<TrapResult>().LoseTrap(this);
        end = true;
    }
    public override void TrapLossEffects()
    {
        return;
        //should reduce heroes damage by 1
    }
}

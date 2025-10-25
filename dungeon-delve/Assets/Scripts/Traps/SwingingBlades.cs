using UnityEngine;
using System.Collections;

public class SwingingBlades : TrapBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetHeroes(Job.ranger);

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Fail();
    }
}

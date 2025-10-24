using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class InfernalFlame : TrapBase
{
    [SerializeField] private float timeToLose = 5f;
    [SerializeField] private int platformsToSpawn = 10;

    private bool end = false;
    private InputAction moveAction;
    private InputAction jumpAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {

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
        //should be blank
    }
}

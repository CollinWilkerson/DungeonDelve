using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hex : TrapBase
{
    private enum actions
    {
        up,
        down,
        left,
        right
    }

    [SerializeField] private float timeToLose = 5f;
    [SerializeField] private int CharsToSpawn = 10;

    private int mages = 1;
    private bool end = false;
    private List<actions> actionList;


    private InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        foreach (MercObject merc in MercObject.Party)
        {
            if (merc != null)
            {
                mages += merc.GetMage();
            }
        }

        //I need to generate a list of commands
        for (int i = 0; i < CharsToSpawn; i++)
        {
            actionList.Add(actions.up); //replace with random action
        }

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        //this will be where the player inputs and it matches the commands
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToLose);

        if (!end)
        {
            FindAnyObjectByType<TrapResult>().WinTrap(this);
            end = true;
        }
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

    public override void TrapLossEffects()
    {
        return;
        //should reduce heroes damage by 1
    }
}

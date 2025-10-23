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
    private Queue<actions> actionQueue;


    [Header("UI")]
    [SerializeField] private Transform expectedInputParent;
    [SerializeField] private Transform actualInputParent;
    //I have some idea of setting these in some master file and changing them based on control type
    [SerializeField] private GameObject upIcon;
    [SerializeField] private GameObject rightIcon;
    [SerializeField] private GameObject downIcon;
    [SerializeField] private GameObject leftIcon;


    private InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        if (MercObject.Party != null)
        {
            foreach (MercObject merc in MercObject.Party)
            {
                if (merc != null)
                {
                    mages += merc.GetMage();
                }
            }
        }

        actionQueue = new Queue<actions>();

        //I need to generate a list of commands
        for (int i = 0; i < CharsToSpawn; i++)
        {
            actionQueue.Enqueue(GetRandomAction());
        }

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAction.triggered)
        {
            InputToAction();
        }
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

    private actions GetRandomAction()
    {
        switch(Random.Range(0, 4))
        {
            case 0:
                Instantiate(upIcon, expectedInputParent);
                return actions.up;
            case 1:
                Instantiate(leftIcon, expectedInputParent);
                return actions.left;
            case 2:
                Instantiate(rightIcon, expectedInputParent);
                return actions.right;
            default:
                Instantiate(downIcon, expectedInputParent);
                return actions.down;
        }
    }

    private actions InputToAction()
    {
        float xInput = moveAction.ReadValue<Vector2>().x;
        float yInput = moveAction.ReadValue<Vector2>().y;

        //this should help resolve issues of gamepad stick having imperfect inupt
        if (Mathf.Abs(xInput) < Mathf.Abs(yInput))
        {
            if(yInput < 0)
            {
                //Debug.Log("Down");
                Instantiate(downIcon, actualInputParent);
                return actions.down;
            }
            //Debug.Log("Up");
            Instantiate(upIcon, actualInputParent);
            return actions.up;
        }
        if (xInput < 0)
        {
            //Debug.Log("Left");
            Instantiate(leftIcon, actualInputParent);
            return actions.left;
        }
        //Debug.Log("Right");
        Instantiate(rightIcon, actualInputParent);
        return actions.right;
    }

    public override void TrapLossEffects()
    {
        return;
        //should reduce heroes damage by 1
    }
}

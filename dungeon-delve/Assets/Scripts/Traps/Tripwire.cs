using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tripwire : TrapBase
{
    [SerializeField] private float fallRate = 1f;
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float maxSpeed = 150f;
    [SerializeField] private float timeToWin = 5f;
    [SerializeField] private float balanceSens = 20f;
    [SerializeField] private float inputDecay = 2f;
    [SerializeField] GameObject pivot;

    private float adjustForce = 0f;
    private float angle = -0.1f;
    //one is our default, it will be the hardest difficulty
    private int rangers = 1;
    private bool end = false;

    private InputAction moveAction;
    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        foreach (MercObject merc in MercObject.Party)
        {
            if (merc != null)
            {
                rangers += merc.GetRanger();
            }
        }
        fallRate = fallRate / rangers;
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (end)
        {
            return;
        }
        //game effect
        float naturalForce = Mathf.Sign(angle) * Mathf.Clamp(Mathf.Abs(angle) * fallRate, minSpeed, maxSpeed) ;
        //player effect
        adjustForce += (-moveAction.ReadValue<Vector2>().x * balanceSens);
        if(moveAction.ReadValue<Vector2>().x == 0)
        {
            adjustForce = Mathf.Lerp(adjustForce, 0, Time.deltaTime * inputDecay);
        }
        angle += (adjustForce + naturalForce) * Time.deltaTime;
        //handle loss
        if (angle > 90 || angle < -90)
        {
            Fail();
        }

        //update UI
        pivot.transform.rotation = Quaternion.Euler(new Vector3(0,0, angle));
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

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToWin);

        if (!end)
        {
            FindAnyObjectByType<TrapResult>().WinTrap(this);
            end = true;
        }
    }

    public override void TrapLossEffects()
    {
        return;
        //should reduce heroes speed by 1
    }
}

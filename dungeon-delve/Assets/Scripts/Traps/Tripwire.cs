using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tripwire : TrapBase
{
    [SerializeField] private float fallRate = 1f;
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float maxSpeed = 150f;
    [SerializeField] private float balanceSens = 20f;
    [SerializeField] private float inputDecay = 2f;
    [SerializeField] GameObject pivot;

    private float adjustForce = 0f;
    private float angle = -0.1f;
    //one is our default, it will be the hardest difficulty

    private InputAction moveAction;
    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        GetHeroes(Job.ranger);
        fallRate = fallRate / heroes;
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
        HandlePlayerInput();
        angle += (adjustForce + naturalForce) * Time.deltaTime;
        //handle loss
        if (angle > 90 || angle < -90)
        {
            Fail();
        }

        //update UI
        pivot.transform.rotation = Quaternion.Euler(new Vector3(0,0, angle));
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Pass();
    }

    public override void TrapLossEffects()
    {
        return;
        //should reduce heroes speed by 1
    }

    private void HandlePlayerInput()
    {
        adjustForce += (-moveAction.ReadValue<Vector2>().x * balanceSens * Time.deltaTime);
        if (moveAction.ReadValue<Vector2>().x == 0)
        {
            adjustForce = Mathf.Lerp(adjustForce, 0, Time.deltaTime * inputDecay);
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class UnevenBars : TrapBase
{
    [Header("Adjustments")]
    [SerializeField] private float spinSpeed;
    [SerializeField] private float sucessRange;

    [Header("UI")]
    [SerializeField] private GameObject spinner;

    private float circleMax = 360;
    private float curCircle = 180;

    private InputAction jumpAction;

    private void Start()
    {
        GetHeroes(Job.ranger);

        if(heroes > 3)
        {
            Pass();
        }

        jumpAction = InputSystem.actions.FindAction("Jump");

        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (end)
        {
            return;
        }

        curCircle = (curCircle + spinSpeed * Time.deltaTime) % circleMax;
        Debug.Log(curCircle);
        spinner.transform.localRotation = Quaternion.Euler(Vector3.forward * curCircle);

        if (jumpAction.triggered)
        {
            if(curCircle < sucessRange/2 || curCircle > 360 - sucessRange / 2)
            {
                Pass();
                return;
            }
            Fail();
            return;
        }
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Fail();
    }
}

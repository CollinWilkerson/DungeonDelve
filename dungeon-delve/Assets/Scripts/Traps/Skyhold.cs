using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Skyhold : TrapBase
{
    private enum action
    {
        space = 0,
        left = 1,
        right = 2,
    }
    [Header("Adjustments")]
    [SerializeField] private float barMax = 100f;
    [SerializeField] private float barPerSec = 50f;
    [SerializeField] private float switchTime = 0.5f;

    [Header("UI")]
    [SerializeField] private Image spaceIcon;
    [SerializeField] private Image rightIcon;
    [SerializeField] private Image leftIcon;
    [SerializeField] private Image barFill;


    private float bar;

    private action curAction;

    private InputAction moveAction;
    private InputAction jumpAction;

    private void Start()
    {
        GetHeroes(Job.warrior);

        if(heroes > 9)
        {
            Pass();
        }

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        bar = barMax;
        barPerSec /= heroes;
        curAction = (action)Random.Range(0, 3);
        //Debug.Log(curAction.ToString());
        ManageActionUI();

        StartCoroutine(SwitchAction());
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (end)
        {
            return;
        }
        if (curAction == action.space && jumpAction.IsPressed())
        {
            //Debug.Log("good");
            return;
        }
        if (curAction == action.right && moveAction.ReadValue<Vector2>().x > 0.9f)
        {
            //Debug.Log("good");
            return;
        }
        if (curAction == action.left && moveAction.ReadValue<Vector2>().x < -0.9f)
        {
            //Debug.Log("good");
            return;
        }

        bar -= barPerSec * Time.deltaTime;
        ManageBarUI();

        if(bar < 0)
        {
            Fail();
        }
    }

    private void ManageBarUI()
    {
        barFill.fillAmount = bar / barMax;
    }

    private void ManageActionUI()
    {
        spaceIcon.color = Color.grey;
        rightIcon.color = Color.grey;
        leftIcon.color = Color.grey;

        switch (curAction)
        {
            case action.space:
                spaceIcon.color = Color.white;
                return;
            case action.right:
                rightIcon.color = Color.white;
                return;
            default:
                leftIcon.color = Color.white;
                return;
        }
    }

    private IEnumerator SwitchAction()
    {
        while (!end)
        {
            yield return new WaitForSeconds(switchTime);
            curAction = (action) Random.Range(0, 3);
            //Debug.Log(curAction.ToString());
            ManageActionUI();
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Pass();
    }

    public override void TrapLossEffects()
    {
        return;
        //should kill the highest health hero
    }
}

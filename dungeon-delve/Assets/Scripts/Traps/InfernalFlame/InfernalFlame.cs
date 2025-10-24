using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class InfernalFlame : TrapBase
{
    [SerializeField] private float timeToLose = 5f;

    [SerializeField] private float speed;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private float cross_width;
    [SerializeField] private float cross_height;
    [SerializeField] private int baseBurnStacks = 10;
    [SerializeField] BurningChar burningChar;

    private int mages = 1;
    private bool end = false;
    private InputAction moveAction;
    private InputAction jumpAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetMages();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        cross_height *= mages;
        cross_width *= mages;
        crosshair.transform.localScale *= mages;
        burningChar.Initialize(baseBurnStacks, new Vector2(0,0), new Vector2(Screen.width, Screen.height));
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        crosshair.transform.position += (Vector3) moveAction.ReadValue<Vector2>() * speed;
        if (jumpAction.triggered && InSquare())
        {
            Pass();
            return;
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

    private void GetMages()
    {
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
    }

    private bool InSquare()
    {
        float char_x = burningChar.transform.position.x;
        float char_y = burningChar.transform.position.y;
        float cross_x = crosshair.transform.position.x;
        float cross_y = crosshair.transform.position.y;
        bool inBoundsX = char_x > cross_x - cross_width / 2 && char_x < cross_x + cross_width;
        bool inBoundsY = char_y > cross_y - cross_height / 2 && char_y < cross_y + cross_height;
        return inBoundsX && inBoundsY;
    }

    public override void TrapLossEffects()
    {
        return;
        //should be blank
    }
}

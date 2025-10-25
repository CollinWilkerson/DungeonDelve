using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Portcullis : TrapBase
{

    [Tooltip("how far out of 100 the portcullis will decend in 1 second")]
    [SerializeField] private float decendRate = 30f;

    [Header("UI")]
    [SerializeField] private RectTransform canvas;
    [SerializeField] private RectTransform warrior;
    [SerializeField] private RectTransform portcullis;

    private float position = 100f;
    //one is our default, it will be the hardest difficulty
    private Vector3 p_initialPosition;
    private Vector3 w_initialScale;

    private InputAction jumpAction;

    private void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");

        GetHeroes(Job.warrior);

        p_initialPosition = portcullis.position;
        w_initialScale = warrior.localScale;

        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (end)
        {
            return;
        }
        //game logic
        position -= decendRate * Time.deltaTime;
        if (jumpAction.triggered)
        {
            position += heroes;
        }
        if (position < 0)
        {
            Fail();
            return;
        }
        //ui update
        portcullis.position = p_initialPosition - (Vector3.up * canvas.rect.height * (1f - position / 100f)) / 1.5f;
        //start 1,1,1
        //end 2,0,1
        // position/ 100f starts at one and decreases to 0
        // 1 - position/100f starts at 0 and increases to 1
        warrior.localScale = w_initialScale + new Vector3(1 - (position / 100f), -(1 - (position / 100f)), 0);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Pass();
    }
}

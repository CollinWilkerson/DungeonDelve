using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Portcullus : MonoBehaviour
{
    [Tooltip("how far out of 100 the portcullus will decend in 1 second")]
    [SerializeField] private float decendRate = 30f;
    [SerializeField] private float timeToWin = 5f;

    private float position = 100f;
    //one is our default, it will be the hardest difficulty
    private int warriors = 1;

    private InputAction jumpAction;

    private void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");

        foreach (MercObject merc in MercObject.Party)
        {
            if (merc != null)
            {
                warriors += merc.GetWarrior();
            }
        }

        StartCoroutine(Timer());
    }

    private void Update()
    {
        position -= decendRate * Time.deltaTime;
        if (jumpAction.triggered)
        {
            position += warriors;
        }
        if(position < 0)
        {
            Fail();
        }
    }

    private void Fail()
    {
        //add the health loss and stuff
        Debug.Log("Player Loses");
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToWin);
        //implement win stuff
        Debug.Log("Player Wins");
        //add gold
        //give treasure
        //change scene
    }
}

using UnityEngine;
using System.Collections;

public class TrapIntro : MonoBehaviour
{
    [SerializeField] private GameObject introScreen;
    [SerializeField] private GameObject trapControl;
    [SerializeField] private float waitTime = 1f;

    private void Start()
    {
        StartCoroutine(StartTrapAfterDelay());
    }

    private IEnumerator StartTrapAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);

        //the goal is to animate this later based on class
        //vertical slash for warrior
        //horizontal cut for ranger
        //wipe for mage
        trapControl.SetActive(true);
        introScreen.SetActive(false);
    }
}

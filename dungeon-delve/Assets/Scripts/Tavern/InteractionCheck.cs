using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI toolTip;

    [Header("Check Variables")]
    [SerializeField] private float angle;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetMask;

    private Transform activeInteractible;
    private InputAction interactAction;

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");

        toolTip.gameObject.SetActive(false);

        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if (interactAction.triggered && activeInteractible)
        {
            activeInteractible.GetComponent<IInteractable>().Interact();
        }
    }

    private IEnumerator FOVRoutine()
    {
        //how often the coroutine runs
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FeildOfViewCheck();
        }
    }

    private void FeildOfViewCheck()
    {
        //this is what actually looks for interactables
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        //If anything is in our array it has picked up an interactable
        if (rangeChecks.Length != 0)
        {
            //the only thing in the targetmask is the player, so we use the first index

            Transform target = null;
            float newDistance;
            float targetDistance = radius * 2; //make targetDistance large so it chooses someone

            //finds the closest target
            foreach (Collider collider in rangeChecks)
            {
                newDistance = Vector3.Distance(transform.position, collider.transform.position);
                if (newDistance < targetDistance)
                {
                    target = collider.transform;
                }
            }
            //establishes direction to enemy rotation to player location
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //gets the angle between the forward direction and the normalized vector to the target and compares it to half the angle we established in the beginning.
            //the angle is halved because half of the angle is to the left and half is to the right
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                toolTip.gameObject.SetActive(true);
                //some thing that allows the player to interact with this closest object
                activeInteractible = target;
            }
            else
            {
                toolTip.gameObject.SetActive(false);
                activeInteractible = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI toolTip;

    [SerializeField] private GameObject PartyMenu;

    [Header("Check Variables")]
    [SerializeField] private float angle;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float selectionThreshould = 0.9f;

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
        else if (interactAction.triggered)
        {
            if (PartyMenu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                PartyMenu.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                PartyMenu.SetActive(true);
            }
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
        
        ClearSelection();

        //If anything is in our array it has picked up an interactable
        if (rangeChecks.Length != 0)
        {
            float bestMatch = 0;
            foreach (Collider interactable in rangeChecks)
            {
                Ray veiwportCenterRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                Vector3 lookVector = veiwportCenterRay.direction;
                Vector3 interactableVector = interactable.transform.position - veiwportCenterRay.origin;

                float lookMatchPercentage = Vector3.Dot(lookVector.normalized, interactableVector.normalized);

                if (IsBestMatch(bestMatch, lookMatchPercentage))
                {
                    bestMatch = lookMatchPercentage;
                    activeInteractible = interactable.transform;
                    toolTip.gameObject.SetActive(true);
                }
            }
        }
    }

    private void ClearSelection()
    {
        activeInteractible = null;
        toolTip.gameObject.SetActive(false);
    }

    private bool IsBestMatch(float bestMatch, float lookMatchPercentage)
    {
        return lookMatchPercentage > selectionThreshould && lookMatchPercentage > bestMatch;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

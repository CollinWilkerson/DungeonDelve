using UnityEngine;

public class HeroInteraction : MonoBehaviour, IInteractable
{
    private int cost;
    private int heroFilePath;

    private void Start()
    {
        SetLayerMask();
    }

    private void Interact() 
    { 
        //open stat and hiring menu
    }

    private void SetLayerMask() 
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}

using UnityEngine;

public interface IInteractable
{
    public static IInteractable highligted;
    public void SetHighligted()
    {
        highligted = this;
    }

    private void SetLayerMask() { }
    private void Interact() { }

}

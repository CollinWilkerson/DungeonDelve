using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private string itemButtonPrefabFilepath;

    [SerializeField] float moveSpeed;

    [SerializeField] private GameObject itemMenu;
    [SerializeField] private float openOffset = 250f;

    private bool open = false;


    public void InteractItemMenu()
    {
        if (itemMenu)
        {
            Debug.Log("Move");
            if (open) 
            {
                itemMenu.transform.position = new Vector2 (itemMenu.transform.position.x, itemMenu.transform.position.y - openOffset);
                open = false;
            }
            else
            {
                itemMenu.transform.position = new Vector2(itemMenu.transform.position.x, itemMenu.transform.position.y + openOffset);
                open = true;
            }
        }
    }
}

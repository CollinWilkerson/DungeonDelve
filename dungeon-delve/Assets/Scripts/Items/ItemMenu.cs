using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private string itemButtonPrefabFilepath;
    [SerializeField] private Transform itemList;

    [SerializeField] private GameObject itemMenu;
    [SerializeField] private float openOffset = 250f;

    private bool open = false;

    private void Start()
    {
        GameObject itemButton;

        foreach(int index in PlayerData.itemInventory)
        {
            if (index != -1)
            {
                itemButton = Instantiate(Resources.Load<GameObject>
                    (itemButtonPrefabFilepath), itemList);
                itemButton.GetComponent<ItemButtonActions>().Initialize(index);
            }
        }
    }

    public void InteractItemMenu()
    {
        if (itemMenu)
        {
            //Debug.Log("Move");
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

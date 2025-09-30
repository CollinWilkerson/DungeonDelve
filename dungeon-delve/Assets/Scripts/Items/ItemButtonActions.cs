using UnityEngine;
using System;
using System.IO;
using TMPro;

public class ItemButtonActions : MonoBehaviour
{
    private int itemIndex;
    private static ItemButtonActions activeItem;
    private IItem item;
    [SerializeField] TextMeshProUGUI nameText;

    public void Initialize(int index)
    {
        itemIndex = index;
        //this creates the item from the set index
        string scriptName = DataFiles.Items[index].Split(',')[1];
        //Debug.Log("Trying to add: " + scriptName);
        item = gameObject.AddComponent(Type.GetType(scriptName)) as IItem;
        nameText.text = item.ReturnName();
    }
    public void ItemClick()
    {
        if (item.HasTarget())
        {
            activeItem = this;
            FindAnyObjectByType<HeroButtonContainer>(FindObjectsInactive.Include).gameObject.SetActive(true);
        }
        else
        {
            item.UseItem(null);
            PlayerData.RemoveItem(itemIndex);
            Destroy(gameObject);
        }
    }

    public static void UseActiveItem(MercenaryController target)
    {
        if (!activeItem)
        {
            return;
        }

        activeItem.item.UseItem(target);
        PlayerData.RemoveItem(activeItem.itemIndex);
        Destroy(activeItem.gameObject);
        activeItem = null;
    }
}

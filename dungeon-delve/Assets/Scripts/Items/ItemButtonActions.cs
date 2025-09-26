using UnityEngine;
using System;
using System.IO;
using TMPro;

public class ItemButtonActions : MonoBehaviour
{
    private IItem item;
    [SerializeField] TextMeshProUGUI nameText;

    public void Initialize(int index)
    {
        //this creates the item from the set index
        string scriptName = File.ReadAllLines(
                "Assets/Resources/Data/items.csv")[index].Split(',')[1];
        //Debug.Log("Trying to add: " + scriptName);
        item = gameObject.AddComponent(Type.GetType(scriptName)) as IItem;
        nameText.text = item.ReturnName();
    }
    public void ItemClick()
    {
        if (item.HasTarget())
        {
            //put buttons on targets or smthn
        }
        else
        {
            item.UseItem(null);
        }
    }
}

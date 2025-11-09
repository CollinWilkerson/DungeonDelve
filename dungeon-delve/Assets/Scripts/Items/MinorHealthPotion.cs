using UnityEngine;

public class MinorHealthPotion : MonoBehaviour, IItem
{
    private int potionStrength = 5;
    public string ReturnName()
    {
        return "Minor Health Potion";
    }
    public bool HasTarget()
    {
        return true;
    }
    public void UseItem(MercenaryController merc)
    {
        merc.Heal(potionStrength);
    }
    public string GetDescription()
    {
        return DataFiles.Items[2].Split(",")[2];
    }
}

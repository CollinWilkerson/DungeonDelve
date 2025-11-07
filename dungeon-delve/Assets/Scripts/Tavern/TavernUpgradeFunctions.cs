using UnityEngine;

public class TavernUpgradeFunctions
{
    private const int baseTableCost = 20;
    private const int baseCostReductionCost = 40;
    private const int shopCost = 50;

    public static bool TryAddTable()
    {
        if (PlayerData.GetGold() > TableCost())
        {
            PlayerData.SpendGold(TableCost());
            TavernData.tables++;
            return true;
        }
        return false;
    }
    public static int TableCost()
    {
        return Mathf.CeilToInt(Mathf.Pow(baseTableCost, TavernData.tables));
    }

    public static bool TryAddCostReduction()
    {
        if (PlayerData.GetGold() > CostReductionCost())
        {
            PlayerData.SpendGold(CostReductionCost());
            TavernData.discountRate *= 0.9f;
            return true;
        }
        return false;
    }

    public static int CostReductionCost() 
    {
        return Mathf.CeilToInt(baseCostReductionCost / TavernData.discountRate);
    }

    public static bool TryAddShop()
    {
        if (PlayerData.GetGold() > CostReductionCost())
        {
            PlayerData.SpendGold(shopCost);
            TavernData.shopPurchased = true;
            //call some method that sets the shop active
            return true;
        }
        return false;
    }
}

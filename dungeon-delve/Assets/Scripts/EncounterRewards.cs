using UnityEngine;

public class EncounterRewards
{
    public static bool lastIsItem;
    public static Equipment lastEquipment;
    public static int lastItem;

    public static void GetTreasure()
    {
        int treasure = WeightedTreasureSelection();
        if (treasure < DataFiles.Items.Length)
        {
            lastIsItem = true;
            lastItem = AddItem();
            return;
        }
        lastIsItem = false;
        lastEquipment = AddEquipment();
    }

    private static Equipment AddEquipment()
    {
        Equipment equipmentToGive;
        if (PlayerData.levelsCleared < PlayerData.level1Cutoff)
        {
            equipmentToGive = new Equipment(Random.Range(1, DataFiles.EqEasyCutoff));
            Equipment.AddEq(equipmentToGive);
            return equipmentToGive;
        }
        if (PlayerData.levelsCleared < PlayerData.level2Cutoff)
        {
            equipmentToGive = new Equipment(Random.Range(DataFiles.EqEasyCutoff, DataFiles.EqMediumCutoff));
            Equipment.AddEq(equipmentToGive);
            return equipmentToGive;
        }
        equipmentToGive = new Equipment(Random.Range(DataFiles.EqMediumCutoff, DataFiles.Eq.Length));
        Equipment.AddEq(equipmentToGive);
        return equipmentToGive;
    }

    private static int AddItem()
    {
        //items seems to think there are 4 items even through there are 3
        int itemToGive = Random.Range(1, DataFiles.Items.Length - 1);
        PlayerData.AddItem(itemToGive);
        return itemToGive;
    }

    private static int WeightedTreasureSelection()
    {
        if(PlayerData.levelsCleared < PlayerData.level1Cutoff)
        {
            return Random.Range(1, DataFiles.Items.Length + DataFiles.EqEasyCutoff);
        }
        if(PlayerData.levelsCleared < PlayerData.level2Cutoff)
        {
            return Random.Range(1, DataFiles.Items.Length + DataFiles.EqMediumCutoff - DataFiles.EqEasyCutoff);
        }
        return Random.Range(1, DataFiles.Items.Length + DataFiles.Eq.Length - DataFiles.EqMediumCutoff);
    }
}

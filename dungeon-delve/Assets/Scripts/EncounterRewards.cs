using UnityEngine;

public class EncounterRewards
{
    public static void GetTreasure()
    {
        int treasure = WeightedTreasureSelection();
        if (treasure < DataFiles.Items.Length)
        {
            AddItem();
            return;
        }
        AddEquipment();
    }

    private static void AddEquipment()
    {
        if (PlayerData.levelsCleared < PlayerData.level1Cutoff)
        {
            Equipment.AddEq(new Equipment(Random.Range(1,DataFiles.EqEasyCutoff)));
            return;
        }
        if (PlayerData.levelsCleared < PlayerData.level2Cutoff)
        {
            Equipment.AddEq(new Equipment(Random.Range(DataFiles.EqEasyCutoff, DataFiles.EqMediumCutoff)));
            return;
        }
        Equipment.AddEq(new Equipment(Random.Range(DataFiles.EqMediumCutoff, DataFiles.Eq.Length)));
    }

    private static void AddItem()
    {
        //items seems to think there are 4 items even through there are 3
        PlayerData.AddItem(Random.Range(1, DataFiles.Items.Length - 1));
        return;
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

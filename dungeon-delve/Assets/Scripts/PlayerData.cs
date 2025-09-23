public class PlayerData
{
    private static int gold = 0;
    private static int tempGold = 0;
    public static IItem[] itemInventory { get; private set; } = new IItem[5];

    /// <summary>
    /// adds temporary gold to be merged in the results screen
    /// </summary>
    /// <param name="amount"></param>
    public static void AddTempGold(int amount)
    {
        tempGold += amount;
    }

    /// <summary>
    /// moves temp gold to gold, for results screens
    /// </summary>
    public static int AddGold()
    {
        gold += tempGold;
        tempGold = 0;
        return gold;
    }

    public static int GetGold()
    {
        return gold;
    }

    public static void SpendGold(int goldToSpend)
    {
        gold -= goldToSpend;
    }

    public static void AddItem(IItem item)
    {
        for (int i = 0; i < itemInventory.Length; i++)
        {
            if (itemInventory[i] == null)
            {
                itemInventory[i] = item;
                return;
            }
        }
    }
}

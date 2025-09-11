public class PlayerData
{
    private static int gold = 0;
    private static int tempGold = 0;

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
}

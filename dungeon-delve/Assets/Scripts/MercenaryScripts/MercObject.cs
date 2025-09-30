public enum Job 
{
    warrior,
    ranger,
    mage
}

public class MercObject
{
    public static MercObject[] Party = new MercObject[4];

    private int health;
    public string filePath { get; private set;}
    public int index;
    public Equipment armor;
    public Equipment weapon;
    
    /// <summary>
    /// creates an object for storing mercenaries
    /// </summary>
    /// <param name="path">the location of the Merc, what comes after 'Resources/'</param>
    public MercObject(string path, int _index) //i eventually want to change this so it just takes the index
    {
        filePath = path;
        health = -99; //-99 indicates that the hero doesn't have their health set yet
        index = _index;
    }

    public static void AddHeroToParty(MercObject merc)
    {
        for(int i = 0; i < Party.Length; i++)
        {
            if(Party[i] == null)
            {
                Party[i] = merc;
                return;
            }
        }
    }

    public static void SwapPartyMembers(int index1, int index2)
    {
        MercObject temp = Party[index2];
        Party[index2] = Party[index1];
        Party[index1] = temp;
    }

    public static void ClearParty()
    {
        for (int i = 0; i < Party.Length; i++)
        {
            Party[i] = null;
        }
    }

    public static void DeletePartyMemeber(int index)
    {
        Party[index] = null;
    }

    public void UpdateHealth(int Health)
    {
        health = Health;
    }

    public int GetHealth()
    {
        return health;
    }
}

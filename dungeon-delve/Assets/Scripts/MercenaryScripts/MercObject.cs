using System;

public enum Job 
{
    warrior,
    ranger,
    mage
}

public class MercObject
{
    public const int PartySize = 4;
    public static MercObject[] Party = new MercObject[PartySize];

    private string name;
    private int health;
    public string filePath { get; private set;}
    public int index;
    public Equipment armor;
    public Equipment weapon;
    
    /// <summary>
    /// creates an object for storing mercenaries
    /// </summary>
    /// <param name="path">the location of the Merc, what comes after 'Resources/'</param>
    public MercObject(string path, int _index, string _name) //i eventually want to change this so it just takes the index
    {
        filePath = path;
        health = -99; //-99 indicates that the hero doesn't have their health set yet
        index = _index;
        name = _name;
    }

    public MercObject(int _index)
    {
        filePath = DataFiles.Heroes[_index].Split(",")[7];
        health = -99;
        index = _index;
        name = DataFiles.GetRandomName();
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

    public int GexMaxHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        int maxHealth = Int32.Parse(DataFiles.Heroes[index].Split(',')[1]);
        if(armor != null)
        {
            maxHealth += armor.GetHealth();
        }
        if(weapon != null)
        {
            maxHealth += weapon.GetHealth();
        }
        if(maxHealth < 1)
        {
            return 1;
        }
        return maxHealth;
    }

    public int GetDamage()
    {
        int damage = Int32.Parse(DataFiles.Heroes[index].Split(',')[2]);
        if(armor != null)
        {
            damage += armor.GetDamage();
        }
        if(weapon != null)
        {
            damage += weapon.GetDamage();
        }
        if(damage < 1)
        {
            return 1;
        }
        return damage;
    }

    public int GetSpeed()
    {
        int speed = Int32.Parse(DataFiles.Heroes[index].Split(',')[3]);
        if (armor != null)
        {
            speed += armor.GetSpeed();
        }
        if (weapon != null)
        {
            speed += weapon.GetSpeed();
        }
        if (speed < 1)
        {
            return 1;
        }
        return speed;
    }

    public int GetWarrior()
    {
        return Int32.Parse(DataFiles.Heroes[index].Split(",")[9]);
    }
    public int GetRanger()
    {
        return Int32.Parse(DataFiles.Heroes[index].Split(",")[10]);
    }
    public int GetMage()
    {
        return Int32.Parse(DataFiles.Heroes[index].Split(",")[11]);
    }

    public string GetName()
    {
        return name;
    }

    public static int GetTotalDamage()
    {
        int total = 0;
        foreach(MercObject merc in Party)
        {
            if(merc == null)
            {
                continue;
            }
            total += merc.GetDamage();
        }
        return total;
    }
}

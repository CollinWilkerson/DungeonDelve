using System;

public enum Eq_Type
{
    armor,
    weapon
}

public class Equipment
{
    private const int inventorySize = 30;

    public static Equipment[] eq_inventory = new Equipment[inventorySize];

    public int index = 1;

    public Equipment(int _index)
    {
        index = _index;
    }

    public static void AddEq(Equipment eq)
    {
        if(eq == null)
        {
            return;
        }

        for(int i = 0; i < eq_inventory.Length; i++)
        {
            if(eq_inventory[i] == null)
            {
                eq_inventory[i] = eq;
                return;
            }
        }
    }
    public static void AddEq(Equipment[] eqArray)
    {
        if(eqArray == null)
        {
            return;
        }

        int i = 0;
        foreach (Equipment eq in eqArray)
        {
            while(i < eq_inventory.Length)
            {
                if (eq_inventory[i] == null)
                {
                    eq_inventory[i] = eq;
                    break;
                }
                i++;
            }
        }
    }
    public static void RemoveEq(Equipment eq)
    {
        for (int i = 0; i < eq_inventory.Length; i++)
        {
            if (eq_inventory[i] == eq)
            {
                eq_inventory[i] = null;
                return;
            }
        }
    }

    public string GetName()
    {
        return DataFiles.Eq[index].Split(',')[0];
    }

    public Eq_Type GetEqType()
    {
        string job = DataFiles.Eq[index].Split(',')[1].ToLower();
        switch (job)
        {
            case ("armor"):
                return Eq_Type.armor;
            default:
                return Eq_Type.weapon;
        }
    }

    public Job GetJob()
    {
        string job = DataFiles.Eq[index].Split(',')[2].ToLower();
        switch (job)
        {
            case ("mage"):
                return Job.mage;
            case ("ranger"):
                return Job.ranger;
            default:
                return Job.warrior;
        }
    }

    public int GetHealth()
    {
        return Int32.Parse(DataFiles.Eq[index].Split(',')[3]);
    }

    public int GetDamage()
    {
        return Int32.Parse(DataFiles.Eq[index].Split(',')[4]);
    }

    public int GetSpeed()
    {
        return Int32.Parse(DataFiles.Eq[index].Split(',')[5]);
    }
}

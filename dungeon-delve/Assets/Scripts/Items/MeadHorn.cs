using UnityEngine;

public class MeadHorn : MonoBehaviour, IItem
{
    public string ReturnName()
    {
        return "Mead Horn";
    }
    public bool HasTarget()
    {
        return false;
    }
    public void UseItem(MercenaryController merc)
    {
        MonsterEncounter encounter;
        if (encounter = FindAnyObjectByType<MonsterEncounter>())
        {
            foreach(HeroController hero in encounter.GetHeroes())
            {
                hero?.DamageBoost(1);
            }
        }
    }

    public string GetDescription()
    {
        return DataFiles.Items[3].Split(",")[2];
    }
}

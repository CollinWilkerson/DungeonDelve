using UnityEngine;

public class EqMenu : MonoBehaviour
{
    [SerializeField] private Transform buttonVLG;

    private bool firstEq;

    public void Initialize(bool isWeapon)
    {
        firstEq = false;
        if (isWeapon)
        {
            GetWeapon();
            return;
        }
        GetArmor();
    }

    private void GetArmor()
    {
        foreach (Equipment eq in Equipment.eq_inventory)
        {
            if (eq?.GetEqType() == Eq_Type.armor)
            {
                JobCompare(eq);
            }
        }
    }

    private void GetWeapon()
    {
        foreach (Equipment eq in Equipment.eq_inventory)
        {
            if (eq?.GetEqType() == Eq_Type.weapon)
            {
                JobCompare(eq);
            }
        }
    }

    private void JobCompare(Equipment eq)
    {
        MercObject merc = EqButtons.activeButton.merc;
        switch (eq.GetJob())
        {
            case (Job.mage):
                if(merc.GetMage() > 0)
                {
                    SpawnEQ(eq);
                }
                return;
            case (Job.ranger):
                if(merc.GetRanger() > 0)
                {
                    SpawnEQ(eq);
                }
                return;
            default:
                if(merc.GetWarrior() > 0)
                {
                    SpawnEQ(eq);
                }
                return;
        }
    }

    private void SpawnEQ(Equipment eq)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("EqButton"), buttonVLG);
        go.GetComponent<EqSelect>().Initialize(eq, gameObject);
        if (!firstEq)
        {
            HighlightedUIManager.SelectUIGameObject(go);
            firstEq = true;
        }
    }
}

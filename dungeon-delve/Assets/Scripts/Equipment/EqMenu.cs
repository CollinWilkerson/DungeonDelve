using UnityEngine;

public class EqMenu : MonoBehaviour
{
    [SerializeField] private Transform buttonVLG;

    public void Initialize(bool isWeapon)
    {
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
            if (eq != null && eq.GetEqType() == Eq_Type.armor)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("EqButton"), buttonVLG);
                go.GetComponent<EqSelect>().Initialize(eq, gameObject);
            }
        }
    }

    private void GetWeapon()
    {
        foreach (Equipment eq in Equipment.eq_inventory)
        {
            if (eq != null && eq.GetEqType() == Eq_Type.weapon)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("EqButton"), buttonVLG);
                go.GetComponent<EqSelect>().Initialize(eq, gameObject);
            }
        }
    }
}

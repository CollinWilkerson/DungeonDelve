using UnityEngine;

public class WeaponMenu: MonoBehaviour
{
    [SerializeField] private Transform buttonVLG;
    private void OnEnable()
    {
        foreach(Equipment eq in Equipment.eq_inventory)
        {
            if (eq != null && eq.GetEqType() == Eq_Type.weapon)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("WeaponButton"), buttonVLG);
                go.GetComponent<WeaponButton>().Initialize(eq, this);
            }
        }
    }
}

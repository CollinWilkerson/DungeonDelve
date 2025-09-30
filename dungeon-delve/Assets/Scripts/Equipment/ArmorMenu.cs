using UnityEngine;

public class ArmorMenu : MonoBehaviour
{
    [SerializeField] private Transform buttonVLG;
    private void OnEnable()
    {
        foreach(Equipment eq in Equipment.eq_inventory)
        {
            if (eq != null && eq.GetEqType() == Eq_Type.armor)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("ArmorButton"), buttonVLG);
                go.GetComponent<ArmorButton>().Initialize(eq, this);
            }
        }
    }
}

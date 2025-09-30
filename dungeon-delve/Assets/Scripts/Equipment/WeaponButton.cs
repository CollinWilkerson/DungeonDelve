using UnityEngine;
using TMPro;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EqName;
    [SerializeField] private TextMeshProUGUI EqHealth;
    [SerializeField] private TextMeshProUGUI EqDamage;
    [SerializeField] private TextMeshProUGUI EqSpeed;

    private Equipment weapon;
    private WeaponMenu menu;
    public void Initialize(Equipment eq, WeaponMenu _menu)
    {
        weapon = eq;
        EqName.text = eq.GetName();
        EqHealth.text = "-" + eq.GetHealth();
        EqDamage.text = "-" + eq.GetDamage();
        EqSpeed.text = "-" + eq.GetSpeed();
        menu = _menu;
    }

    public void OnClick()
    {
        EqButtons.activeMerc.weapon = weapon;
        Equipment.RemoveEq(weapon);
        menu.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}

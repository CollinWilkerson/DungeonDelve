using UnityEngine;
using TMPro;

public class ArmorButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EqName;
    [SerializeField] private TextMeshProUGUI EqHealth;
    [SerializeField] private TextMeshProUGUI EqDamage;
    [SerializeField] private TextMeshProUGUI EqSpeed;

    private Equipment armor;
    private ArmorMenu menu;
    public void Initialize(Equipment eq, ArmorMenu _menu)
    {
        armor = eq;
        EqName.text = eq.GetName();
        EqHealth.text = "-" + eq.GetHealth();
        EqDamage.text = "-" + eq.GetDamage();
        EqSpeed.text = "-" + eq.GetSpeed();
        menu = _menu;
    }

    public void OnClick()
    {
        EqButtons.activeButton.SetArmor(armor);
        Equipment.RemoveEq(armor);
        menu.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}

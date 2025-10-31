using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EqSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EqName;
    [SerializeField] private TextMeshProUGUI EqHealth;
    [SerializeField] private TextMeshProUGUI EqDamage;
    [SerializeField] private TextMeshProUGUI EqSpeed;
    [SerializeField] private Image image;

    private Equipment equipment;
    private GameObject menu;
    public void Initialize(Equipment eq, GameObject _menu)
    {
        equipment = eq;
        EqName.text = eq.GetName();
        EqHealth.text = "" + eq.GetHealth();
        EqDamage.text = "" + eq.GetDamage();
        EqSpeed.text = "" + eq.GetSpeed();
        image.sprite = eq.GetSprite();
        menu = _menu;
    }

    public void OnClick()
    {
        Equipment.RemoveEq(equipment);
        menu.SetActive(false);
        if (equipment.GetEqType() == Eq_Type.armor)
        {
            EqButtons.activeButton.SetArmor(equipment);
            return;
        }
        EqButtons.activeButton.SetWeapon(equipment);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}

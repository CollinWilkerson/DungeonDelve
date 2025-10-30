using UnityEngine;

public class EqButtons : MonoBehaviour
{
    public static EqButtons activeButton;
    private GameObject armorMenu;
    private GameObject weaponMenu;
    private MercObject merc;

    public void Initialize(MercObject _merc)
    {
        merc = _merc;
        armorMenu = FindAnyObjectByType<ArmorMenu>(FindObjectsInactive.Include).gameObject;
        weaponMenu = FindAnyObjectByType<WeaponMenu>(FindObjectsInactive.Include).gameObject;
    }

    public void SetArmor(Equipment armor)
    {
        merc.armor = armor;
        //Change images
        //Update character stats
    }

    public void SetWeapon(Equipment weapon)
    {
        merc.weapon = weapon;
        //Change images
        //Update character stats
    }

    public void OnClickArmorSlot()
    {
        activeButton = this;
        armorMenu.SetActive(true);
        weaponMenu.SetActive(false);
    }

    public void OnClickWeaponSlot()
    {
        activeButton = this;
        armorMenu.SetActive(false);
        weaponMenu.SetActive(true);
    }
}

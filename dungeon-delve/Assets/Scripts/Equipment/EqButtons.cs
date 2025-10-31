using UnityEngine;

public class EqButtons : MonoBehaviour
{
    public static EqButtons activeButton;
    private EqMenu eqMenu;
    private MercObject merc;

    public void Initialize(MercObject _merc)
    {
        merc = _merc;
        eqMenu = FindAnyObjectByType<EqMenu>(FindObjectsInactive.Include);
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
        eqMenu.gameObject.SetActive(false); //this is nessasary i swear
        eqMenu.gameObject.SetActive(true);
        eqMenu.Initialize(false);
    }

    public void OnClickWeaponSlot()
    {
        activeButton = this;
        eqMenu.gameObject.SetActive(false); //this is nessasary i swear
        eqMenu.gameObject.SetActive(true);
        eqMenu.Initialize(true);
    }
}

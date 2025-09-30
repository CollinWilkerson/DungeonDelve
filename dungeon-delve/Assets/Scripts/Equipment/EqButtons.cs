using UnityEngine;

public class EqButtons : MonoBehaviour
{
    public static MercObject activeMerc;
    private MercObject merc;
    private GameObject armorMenu;
    private GameObject weaponMenu;

    public void Initialize(MercObject _merc)
    {
        //Debug.Log("Initialized");
        merc = _merc;
        armorMenu = FindAnyObjectByType<ArmorMenu>(FindObjectsInactive.Include).gameObject;
        weaponMenu = FindAnyObjectByType<WeaponMenu>(FindObjectsInactive.Include).gameObject;
    }
    public void OnClickArmorSlot()
    {
        activeMerc = merc;
        armorMenu.SetActive(true);
        weaponMenu.SetActive(false);
    }

    public void OnClickWeaponSlot()
    {
        activeMerc = merc;
        armorMenu.SetActive(false);
        weaponMenu.SetActive(true);
    }
}

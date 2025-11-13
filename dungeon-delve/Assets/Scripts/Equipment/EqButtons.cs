using UnityEngine;
using UnityEngine.UI;

public class EqButtons : MonoBehaviour
{
    [SerializeField] private Image WeaponImage;
    [SerializeField] private Image ArmorImage;

    public static EqButtons activeButton;
    private EqMenu eqMenu;
    public MercObject merc { get; private set; }

    public void Initialize(MercObject _merc)
    {
        merc = _merc;
        eqMenu = FindAnyObjectByType<EqMenu>(FindObjectsInactive.Include);
        RefreshImages();
    }

    public void SetArmor(Equipment armor)
    {
        if(merc.armor != null)
        {
            Equipment.AddEq(merc.armor);
        }
        merc.armor = armor;
        //Change images
        ArmorImage.sprite = armor.GetSprite();
        //Update character stats
        gameObject.GetComponent<HeroContainerBehavior>().SetText(merc);
    }

    public void SetWeapon(Equipment weapon)
    {
        if (merc.weapon != null)
        {
            Equipment.AddEq(merc.weapon);
        }
        merc.weapon = weapon;
        //Change images
        WeaponImage.sprite = weapon.GetSprite();
        //Update character stats
        gameObject.GetComponent<HeroContainerBehavior>().SetText(merc);
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

    private void RefreshImages()
    {
        WeaponImage.sprite = merc.weapon?.GetSprite();
        ArmorImage.sprite = merc.armor?.GetSprite();
    }
}

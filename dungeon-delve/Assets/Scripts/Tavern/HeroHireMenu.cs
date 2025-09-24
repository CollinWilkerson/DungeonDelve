using UnityEngine;
using TMPro;

public class HeroHireMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heroNameText;
    [SerializeField] private TextMeshProUGUI heroClassText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI abilityText;
    [SerializeField] private TextMeshProUGUI flavorText;
    [SerializeField] private TextMeshProUGUI costText;
    private int cost;
    private int index;
    private string filePath;
    private GameObject sceneObject;

    public void OpenMenu(string heroName, string heroClass, int damage,
        int health, int speed, string ability, string flavor, int _cost, string _filePath, GameObject _sceneObject, int _index)
    {

        heroNameText.text = heroName;
        heroClassText.text = heroClass;
        damageText.text = damage + "";
        healthText.text = health + "";
        speedText.text = speed + "";
        if (ability.Equals(""))
        {
            abilityText.gameObject.SetActive(false);
        }
        else
        {
            abilityText.gameObject.SetActive(true);
            abilityText.text = ability;
        }
        flavorText.text = flavor;
        costText.text = _cost + "G";
        cost = _cost;
        filePath = _filePath;
        sceneObject = _sceneObject;
        index = _index;
    }

    public void TryHire()
    {
        if(PlayerData.GetGold() >= cost)
        {
            Debug.Log("hired");
            PlayerData.SpendGold(cost);
            MercObject.AddHeroToParty(new MercObject(filePath, index));
            FindAnyObjectByType<GoldDisplay>().UpdateGoldText();
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(sceneObject);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Poor");
        }
    }
}

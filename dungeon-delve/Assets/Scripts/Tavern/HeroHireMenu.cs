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
    [SerializeField] private GameObject HireButton;
    private int cost;
    private int index;
    private string heroName;
    private string filePath;
    private GameObject sceneObject;

    public void OpenMenu(string heroName, string heroClass, int damage,
        int health, int speed, string ability, string flavor, int _cost, string _filePath, GameObject _sceneObject, int _index)
    {

        heroNameText.text = heroName;
        this.heroName = heroName;
        heroClassText.text = heroClass;
        healthText.text = "Health - " + health;
        damageText.text = "Damage - " + damage;
        speedText.text = "Speed - " + speed;
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
        costText.text = "Hire - " + _cost + "G";
        cost = _cost;
        filePath = _filePath;
        sceneObject = _sceneObject;
        index = _index;

        HighlightedUIManager.SelectUIGameObject(HireButton);
    }

    public void TryHire()
    {
        if(PlayerData.GetGold() >= cost)
        {
            //Debug.Log("hired");
            PlayerData.SpendGold(cost);
            MercObject.AddHeroToParty(new MercObject(filePath, index,heroName));
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

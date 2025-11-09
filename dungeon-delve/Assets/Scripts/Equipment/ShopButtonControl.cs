using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EqName;
    [SerializeField] private TextMeshProUGUI EqHealth;
    [SerializeField] private TextMeshProUGUI EqDamage;
    [SerializeField] private TextMeshProUGUI EqSpeed;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private Image image;


    public void Initialize(Equipment eq, int cost)
    {
        EqName.text = eq.GetName();
        EqHealth.text = "" + eq.GetHealth();
        EqDamage.text = "" + eq.GetDamage();
        EqSpeed.text = "" + eq.GetSpeed();
        CostText.text = cost + "G";
        image.sprite = eq.GetSprite();
    }
}

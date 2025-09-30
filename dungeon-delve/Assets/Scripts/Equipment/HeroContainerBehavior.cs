using UnityEngine;
using TMPro;
using System.IO;

public class HeroContainerBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;

    private MercObject merc;

    public void Initialize(MercObject _merc)
    {
        merc = _merc;
        string[] values = File.ReadAllLines(
                "Assets/Resources/Data/heroStats.csv")[merc.index].Split(',');
        healthText.text = values[1];
        damageText.text = values[2];
        speedText.text =  values[3];
        gameObject.GetComponent<EqButtons>().Initialize(_merc);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}

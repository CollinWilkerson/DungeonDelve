using UnityEngine;
using TMPro;
using System;

public class HeroContainerBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;

    public void Initialize(MercObject _merc)
    {
        SetText(_merc);
        gameObject.GetComponent<EqButtons>().Initialize(_merc);
    }

    public void SetText(MercObject _merc)
    {
        healthText.text = _merc.GetMaxHealth().ToString();
        damageText.text = _merc.GetDamage().ToString();
        speedText.text = _merc.GetSpeed().ToString();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}

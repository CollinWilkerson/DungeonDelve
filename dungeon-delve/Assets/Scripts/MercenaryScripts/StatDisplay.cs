using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image speedBar;
    
    private int maxHealth;

    public void SetHealthbar(int _maxHealth)
    {
        maxHealth = _maxHealth;
        UpdateHealthbar(maxHealth);
    }

    public void UpdateHealthbar(int health)
    {
        healthText.text = health + "/" + maxHealth;
        healthBar.fillAmount = (float)health / maxHealth;
    }

    public void UpdateSpeedBar(int time)
    {
        speedBar.fillAmount = (float)time / 100;
    }
}

using UnityEngine;

public class DamageReductionDefence: MonoBehaviour, IDefend
{
    [SerializeField] private int damageReduction = 1;
    MercenaryController myMercenary;

    public void Initialize(MercenaryController controller)
    {
        myMercenary = controller;
    }

    public void Defend(int damage)
    {
        damage -= damageReduction;
        myMercenary.TakeDamage(damage);
    }
}

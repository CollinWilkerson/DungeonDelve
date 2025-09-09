using UnityEngine;

public class DefaultDefend : MonoBehaviour, IDefend
{
    MercenaryController myMercenary;

    public void Initialize(MercenaryController controller)
    {
        myMercenary = controller;
    }

    public void Defend(int damage)
    {
        myMercenary.TakeDamage(damage);
    }
}

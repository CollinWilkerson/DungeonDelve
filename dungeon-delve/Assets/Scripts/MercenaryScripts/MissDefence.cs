using UnityEngine;

public class MissDefence : MonoBehaviour, IDefend
{
    [SerializeField] int HitOutOfInt = 2;
    MercenaryController myMercenary;

    public void Initialize(MercenaryController controller)
    {
        myMercenary = controller;
    }

    public void Defend(int damage)
    {
        if (Random.Range(0, HitOutOfInt) == 0)
        {
            myMercenary.TakeDamage(damage);
        }
    }
}

using UnityEngine;

public class DefaultAttack: MonoBehaviour,IAttack
{
    public void Attack(MercenaryController target, int damage)
    {
        target.defenceControl.Defend(damage);
    }
}

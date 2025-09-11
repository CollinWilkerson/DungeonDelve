using UnityEngine;

public class DefaultAttack: MonoBehaviour,IAttack
{
    public void Attack(MercenaryController target, int damage)
    {
        if(!target)
            return;
        target.defenceControl.Defend(damage);
    }
}

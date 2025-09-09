using UnityEngine;

public interface IDefend
{
    /// <summary>
    /// pass the mercenary controller that will be reciving damage
    /// </summary>
    /// <param name="controller"></param>
    public void Initialize(MercenaryController controller);

    /// <summary>
    /// Manipulate the damage this merc will be reciving before passing it to the MercanaryController
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>the amount of damage to be taken</returns>
    public void Defend(int damage);
}

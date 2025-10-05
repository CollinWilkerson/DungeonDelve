using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    public static TrapBase activeTrap;
    public int goldValue = 2;
    public int damage = 0;

    //not really sure how to implement this without touching everything so im leaving it out rn
    public abstract void TrapLossEffects();
}

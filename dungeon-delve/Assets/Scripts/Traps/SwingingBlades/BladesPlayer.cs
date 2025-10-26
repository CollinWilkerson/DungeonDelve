using UnityEngine;

public class BladesPlayer : MonoBehaviour
{
    private TrapBase swingingBlades;

    public void Initialize(TrapBase b)
    {
        swingingBlades = b;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision");
        if (collision.gameObject.CompareTag("Deathbox"))
        {
            swingingBlades.Fail();
        }
    }
}

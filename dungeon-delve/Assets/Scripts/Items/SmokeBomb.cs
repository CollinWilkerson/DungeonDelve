using UnityEngine;
using UnityEngine.SceneManagement;

public class SmokeBomb : MonoBehaviour,IItem
{
    public string ReturnName()
    {
        return "Smoke Bomb";
    }
    public bool HasTarget()
    {
        return false;
    }
    public void UseItem(MercenaryController merc)
    {
        SceneManager.LoadScene("Tavern");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SmokeBomb : MonoBehaviour,IItem
{
    public string GetName()
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
    public string GetDescription()
    {
        return DataFiles.Items[1].Split(",")[2];
    }
}

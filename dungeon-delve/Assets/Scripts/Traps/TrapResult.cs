using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class TrapResult : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    [Header("Loss")]
    [SerializeField] private GameObject lossScreen;
    [SerializeField] private TextMeshProUGUI damageText;

    private List<Equipment> deadEq = new List<Equipment>();

    private void Start()
    {
        winScreen.SetActive(false);
        lossScreen.SetActive(false);
    }

    public void WinTrap(TrapBase trap)
    {
        winScreen.SetActive(true);
        HighlightedUIManager.SelectUIGameObject(winScreen.GetComponentInChildren<Button>().gameObject);
        PlayerData.AddTempGold(trap.goldValue);
        //give treasure
        EncounterRewards.GetTreasure();
    }

    public void LoseTrap(TrapBase trap)
    {
        lossScreen.SetActive(true);
        HighlightedUIManager.SelectUIGameObject(lossScreen.GetComponentInChildren<Button>().gameObject);
        int i = 0;
        foreach (MercObject merc in MercObject.Party)
        {
            if(merc == null)
            {
                continue;
            }
           // Debug.Log("party member " + i + " with " + merc.GetHealth() + " is taking " + trap.damage + " damage");
            merc.UpdateHealth(merc.GetHealth() - trap.damage);
            if(merc.GetHealth() <= 0)
            {
                if (merc.armor != null)
                {
                    deadEq.Add(merc.armor);
                }
                if (merc.weapon != null)
                {
                    deadEq.Add(merc.weapon);
                }
                MercObject.DeletePartyMemeber(i);
            }
            i++;
        }
        //if party takes damage theres a chance that the first party member died, so we guarentee here
        if(MercObject.Party[0] == null)
        {
            for(i = 1; i < MercObject.Party.Length; i++)
            {
                if(MercObject.Party[i] != null)
                {
                    MercObject.SwapPartyMembers(0, i);
                    break;
                }
            }
        }

        damageText.text = "Your party takes " + trap.damage + " damage";
    }

    //scene transition button control
    public void ToSceneTransition()
    {
        //since we always guarentee that the first slot is filled, if it is not then everyone is dead
        if (MercObject.Party[0] != null)
        {
            Equipment.AddEq(deadEq.ToArray());
            Equipment.AddEq(LostEquipment.GetLostEquipment(PlayerData.levelsCleared));
            PlayerData.levelsCleared ++;
            SceneManager.LoadScene("EncounterWin");
            return;
        }
        LostEquipment.Insert(PlayerData.levelsCleared ,deadEq.ToArray());
        SceneManager.LoadScene("EncounterLoss");    
    }
}

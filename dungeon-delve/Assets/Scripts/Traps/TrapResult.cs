using UnityEngine;
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
        PlayerData.AddTempGold(trap.goldValue);
        //give treasure
        int treasure = Random.Range(1, DataFiles.Items.Length + DataFiles.Eq.Length - 1);
        if(treasure < DataFiles.Items.Length)
        {
            PlayerData.AddItem(treasure);
            return;
        }
        //if I did my math worng this could select the first element of Equipment which is headers
        Equipment.AddEq(new Equipment(treasure - DataFiles.Items.Length));

    }

    public void LoseTrap(TrapBase trap)
    {
        lossScreen.SetActive(true);
        int i = 0;
        foreach (MercObject merc in MercObject.Party)
        {
            if(merc == null)
            {
                continue;
            }
            Debug.Log("party member " + i + " with " + merc.GetHealth() + " is taking " + trap.damage + " damage");
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

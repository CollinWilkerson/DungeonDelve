using UnityEngine;
using System;
using System.IO;
using System.Net.NetworkInformation;

public class HeroInteraction : MonoBehaviour, IInteractable
{
    //the heroes index in heroStats.csv starting at 1
    [SerializeField] private int index = 1;
    private float discountMultiplier = 1;
    private static HeroHireMenu hireMenu;
    private string heroName;

    private void Awake()
    {
        SetLayerMask();
        heroName = DataFiles.GetRandomName();
    }
    private void Start()
    {
        if (hireMenu == null)
        {
            hireMenu = FindAnyObjectByType<HeroHireMenu>(FindObjectsInactive.Include);
        }
    }

    public void Interact() 
    {
        //open stat and hiring menu
        if (!hireMenu.gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;

            hireMenu.gameObject.SetActive(true);
            string line = DataFiles.Heroes[index]; //read heroStats at the important line
            string[] values = line.Split(','); //split csv values by comma (crazy i know)
                                               //pass values from csv to game
            hireMenu.OpenMenu(heroName, values[0], Int32.Parse(values[1]), Int32.Parse(values[2]),
                Int32.Parse(values[3]), values[4], values[5], Mathf.CeilToInt(Int32.Parse(values[6]) * discountMultiplier), values[7], gameObject, index);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            hireMenu.gameObject.SetActive(false);
        }
    }

    public void DiscountSurvivingHero()
    {
        discountMultiplier = 0.5f;
    }

    private void SetLayerMask() 
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    private string MakeRandomName()
    {
        string RandomName = "Joe Battle";
        //temporary return
        return RandomName;
    }

    public void SetName(string _heroName)
    {
        heroName = _heroName;
    }
}

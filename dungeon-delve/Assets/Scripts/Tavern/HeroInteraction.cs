using UnityEngine;
using System;
using System.IO;

public class HeroInteraction : MonoBehaviour, IInteractable
{
    //the heroes index in heroStats.csv starting at 1
    [SerializeField] private int index = 1;
    private static HeroHireMenu hireMenu;

    private void Start()
    {
        SetLayerMask();
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
            string line = File.ReadAllLines("Assets/Data/heroStats.csv")[index]; //read heroStats at the important line
            string[] values = line.Split(','); //split csv values by comma (crazy i know)
                                               //pass values from csv to game
            hireMenu.OpenMenu("Undef", values[0], Int32.Parse(values[1]), Int32.Parse(values[2]),
                Int32.Parse(values[3]), values[4], values[5], Int32.Parse(values[6]), values[7], gameObject);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            hireMenu.gameObject.SetActive(false);
        }
    }

    private void SetLayerMask() 
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}

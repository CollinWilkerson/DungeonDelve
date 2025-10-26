using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Illusion : TrapBase
{
    [SerializeField] private GameObject buttonParent;
    [SerializeField] private Sprite goodSprite;
    [SerializeField] private Sprite badSprite;
    private void Start()
    {
        GetHeroes(Job.mage);

        if(heroes > 9)
        {
            Pass();
        }

        time *= heroes;

        InitButtons();
        StartCoroutine(Timer());
    }

    private void InitButtons()
    {
        List<Button> temp = new List<Button>();
        foreach(Button b in buttonParent.GetComponentsInChildren<Button>())
        {
            temp.Add(b);
        }
        while (temp.Count > 1)
        {
            int j = Random.Range(0, temp.Count);
            temp[j].image.sprite = badSprite;
            temp[j].onClick.AddListener(delegate { Fail(); });
            temp.RemoveAt(j);
        }
        temp[0].image.sprite = goodSprite;
        temp[0].onClick.AddListener(delegate { Pass(); });
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Fail();
    }

    public override void TrapLossEffects()
    {
        //all of your heroes damage one
        return;
    }
}

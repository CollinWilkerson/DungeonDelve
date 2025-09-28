using UnityEngine;

public class HeroButtonContainer : MonoBehaviour
{
    [SerializeField] private HeroBtnController[] buttons;
    private int tail = 0;

    public void RegisterHero(HeroController hero)
    {
        Debug.Log("call");
        if (tail >= buttons.Length)
            return;
        Debug.Log("register");
        buttons[tail].SetTarget(hero);
        buttons[tail].button.interactable = true;
        tail++;
    }
    
}

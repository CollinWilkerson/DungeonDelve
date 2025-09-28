using UnityEngine;
using UnityEngine.UI;

public class HeroBtnController : MonoBehaviour
{
    private HeroController heroTarget;
    public Button button;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.interactable = false;
    }

    public void SetTarget(HeroController target)
    {
        heroTarget = target;
    }

    public void OnClick()
    {
        ItemButtonActions.UseActiveItem(heroTarget);
    }
}

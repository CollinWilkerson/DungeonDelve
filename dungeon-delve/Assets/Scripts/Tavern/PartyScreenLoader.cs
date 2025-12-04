using UnityEngine;
using UnityEngine.UI;

public class PartyScreenLoader : MonoBehaviour
{
    [SerializeField] private GameObject HeroContainerPrefab;
    [SerializeField] private Transform PartyVLG;

    [SerializeField] private GameObject[] DependantMenues;
    private void OnEnable()
    {
        int partyIndex = 0;
        foreach(MercObject merc in MercObject.Party)
        {
            if(merc != null)
            {
                GameObject go = Instantiate(HeroContainerPrefab, PartyVLG);
                go.GetComponent<HeroContainerBehavior>().Initialize(merc, partyIndex);
                if(partyIndex == 0)
                {
                    HighlightedUIManager.SelectUIGameObject(go);
                }
                partyIndex++;
            }
        }
    }

    private void OnDisable()
    {
        foreach(GameObject go in DependantMenues)
        {
            go.SetActive(false);
        }
    }
}

using UnityEngine;

public class PartyScreenLoader : MonoBehaviour
{
    [SerializeField] private GameObject HeroContainerPrefab;
    [SerializeField] private Transform PartyVLG;

    [SerializeField] private GameObject[] DependantMenues;
    private void OnEnable()
    {
        foreach(MercObject merc in MercObject.Party)
        {
            if(merc != null)
            {
                GameObject go = Instantiate(HeroContainerPrefab, PartyVLG);
                go.GetComponent<HeroContainerBehavior>().Initialize(merc);
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

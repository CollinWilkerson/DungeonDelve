using UnityEngine;
public class PartyOrderSwapper
{
    private static int heroIndex1 = -1;

    public static void OnClick(int heroIndex)
    {
        if (heroIndex1 == -1)
        {
            heroIndex1 = heroIndex;
            return;
        }
        MercObject.SwapPartyMembers(heroIndex1, heroIndex);
        heroIndex1 = -1;
        RefreshPartyScreen();
    }

    private static void RefreshPartyScreen()
    {
        GameObject go = GameObject.FindAnyObjectByType<PartyScreenLoader>().gameObject;
        go.SetActive(false);
        go.SetActive(true);
    }
}

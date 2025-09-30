using UnityEngine;

public class DataFiles : MonoBehaviour
{
    [SerializeField] private TextAsset HeroData;
    [SerializeField] private TextAsset MonsterData;
    [SerializeField] private TextAsset ItemData;
    [SerializeField] private TextAsset EqData;

    private static DataFiles instance;

    public static string[] Heroes;
    public static string[] Monsters;
    public static string[] Items;
    public static string[] Eq;

    private void Awake()
    {
        //its a singleton
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        Heroes = HeroData.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        Monsters = MonsterData.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        Items = ItemData.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        Eq = EqData.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
    }
}

using UnityEngine;

public class DataInstance : MonoBehaviour
{
    [Header("CSV Data files")]
    [SerializeField] private TextAsset HeroData;
    [SerializeField] private TextAsset MonsterData; //monster difficulties are selected via prefab path
    [SerializeField] private TextAsset ItemData;
    [SerializeField] private TextAsset EqData;

    private static DataInstance instance;

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

        DataFiles.Heroes = HeroData.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        DataFiles.Monsters = MonsterData.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        DataFiles.Items = ItemData.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        DataFiles.Eq = EqData.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
    }

    public static string SelectEncounter()
    {
        return DataFiles.SelectEncounter();
    }

    public static GameObject SelectBoss()
    {
        return DataFiles.SelectBoss();
    }
    public static string GetRandomName()
    {
        return DataFiles.GetRandomName();
    }
}

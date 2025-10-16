using UnityEngine;

public class DataFiles : MonoBehaviour
{
    [Header("CSV Data files")]
    [SerializeField] private TextAsset HeroData;
    [SerializeField] private TextAsset MonsterData; //monster difficulties are selected via prefab path
    [SerializeField] private TextAsset ItemData;
    [SerializeField] private TextAsset EqData;

    private static DataFiles instance;

    public static string[] Heroes;
    public static string[] Monsters;
    public static string[] Items;
    public static string[] Eq;

    //encounters
    private static int monsterSelectionWeight = 4;
    private static int trapSelectionWeight = 1;
    private static int eventSelectionWeight = 0;
    
    private static string monsterSceneName = "MonsterEncounter"; //monsters are randomly selected in the scene because they all have the same behavior
    private static string[] trapSceneNames = {"Portcullis"}; //traps are devided into different scenes because they have different GUIs and different behaviors
    private static string eventSceneName; //events should be able to have the same behavior, they are all trade off choices

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

    //could update this to take difficulty into account later
    public static string SelectEncounter()
    {
        int encounterFlavor = Random.Range(0, monsterSelectionWeight + 
            trapSelectionWeight + eventSelectionWeight);
        if(encounterFlavor < monsterSelectionWeight)
        {
            return monsterSceneName;
        }
        if(encounterFlavor > monsterSelectionWeight + trapSelectionWeight)
        {
            return eventSceneName;
        }

        return trapSceneNames[Random.Range(0, trapSceneNames.Length)];
    }
}

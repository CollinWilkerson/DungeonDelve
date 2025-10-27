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

    //encounters lv 1
    private static int monsterSelectionWeight = 4;
    private static int trapSelectionWeight = 3;
    private static int eventSelectionWeight = 0;
    
    private static string monsterSceneName = "MonsterEncounter"; //monsters are randomly selected in the scene because they all have the same behavior
    private static string[] trapSceneNames = {"Portcullis", "Tripwire", "Hex", "UnevenBars"}; //traps are devided into different scenes because they have different GUIs and different behaviors
    private static string eventSceneName; //events should be able to have the same behavior, they are all trade off choices
    public const string bossEncouterName = "BossEncounter";
    public const string winScreenName = "WinScreen";

    //encounters lv 2
    private static int lv2_monsterSelectionWeight = 4;
    private static int lv2_trapSelectionWeight = 3;
    private static int lv2_eventSelectionWeight = 0;

    private static string[] lv2_trapSceneNames = { "Pitfall", "InfernalFlame", "SwingingBlades" };

    //encounters lv 3
    private static int lv3_monsterSelectionWeight = 4;
    private static int lv3_trapSelectionWeight = 3;
    private static int lv3_eventSelectionWeight = 0;

    private static string[] lv3_trapSceneNames = { "Explosives", "Illusion", "Skyhold" };

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
        if (PlayerData.levelsCleared + 1 == PlayerData.level1Cutoff || PlayerData.levelsCleared + 1 == PlayerData.level2Cutoff || PlayerData.levelsCleared + 1 == PlayerData.level3Cutoff)
        {
            return bossEncouterName;
        }

        if (PlayerData.levelsCleared + 1 < PlayerData.level1Cutoff)
        {
            return SelectLv1Encounter();
        }
        if (PlayerData.levelsCleared + 1 < PlayerData.level2Cutoff)
        {
            //adjust with lv 2 scene names and weights
            return SelectLv2Encounter();
        }
        if(PlayerData.levelsCleared > PlayerData.level3Cutoff)
        {
            return winScreenName;
        }

        //adjust with lv 3 scene names and weights
        return SelectLv3Encounter();
    }

    private static string SelectLv1Encounter()
    {


        int encounterFlavor = Random.Range(0, monsterSelectionWeight +
            trapSelectionWeight + eventSelectionWeight);
        if (encounterFlavor < monsterSelectionWeight)
        {
            return monsterSceneName;
        }
        if (encounterFlavor > monsterSelectionWeight + trapSelectionWeight)
        {
            return eventSceneName;
        }

        return trapSceneNames[Random.Range(0, trapSceneNames.Length)];
    }

    private static string SelectLv2Encounter()
    {


        int encounterFlavor = Random.Range(0, lv2_monsterSelectionWeight +
            lv2_trapSelectionWeight + lv2_eventSelectionWeight);
        if (encounterFlavor < lv2_monsterSelectionWeight)
        {
            return monsterSceneName;
        }
        if (encounterFlavor > lv2_monsterSelectionWeight + lv2_trapSelectionWeight)
        {
            return eventSceneName;
        }

        return lv2_trapSceneNames[Random.Range(0, lv2_trapSceneNames.Length)];
    }

    private static string SelectLv3Encounter()
    {


        int encounterFlavor = Random.Range(0, lv3_monsterSelectionWeight +
            lv3_trapSelectionWeight + lv3_eventSelectionWeight);
        if (encounterFlavor < lv3_monsterSelectionWeight)
        {
            return monsterSceneName;
        }
        if (encounterFlavor > lv3_monsterSelectionWeight + lv3_trapSelectionWeight)
        {
            return eventSceneName;
        }

        return lv3_trapSceneNames[Random.Range(0, lv3_trapSceneNames.Length)];
    }

    public static GameObject SelectBoss()
    {
        if (PlayerData.levelsCleared + 1 == PlayerData.level1Cutoff)
        { //replace with lv1 boss
            return Resources.Load<GameObject>("Bosses/GoblinKing");
        }
        if (PlayerData.levelsCleared + 1 == PlayerData.level2Cutoff)
        {   //replace with lv 2 boss
            return Resources.Load<GameObject>("Bosses/Raum");
        }
        //replace with boss monster
        return Resources.Load<GameObject>("Bosses/Behemoth");
    }

    public static GameObject SelectMinon()
    {
        if (PlayerData.levelsCleared + 1 == PlayerData.level1Cutoff)
        { //replace with lv1 boss
            return Resources.Load<GameObject>("MonstersLv1/Goblin");
        }
        if (PlayerData.levelsCleared + 1 == PlayerData.level2Cutoff)
        {   //replace with lv 2 boss
            return Resources.Load<GameObject>("MonstersLv2/Vetala");
        }
        //replace with boss monster
        return Resources.Load<GameObject>("MonstersLv3/Saratan");
    }
}

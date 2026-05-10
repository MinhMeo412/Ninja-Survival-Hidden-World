using System.Net.Sockets;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    [Header("RunSelectionData")]
    public CharacterProfile selectedCharacter;
    public int selectedMap;
    public EnemyList enemyList;
    //public GameModeType selectedMode;

    [Header("RunProgressData")]
    public int level;
    public float elapsedTime;
    public int killCount;
    public bool isActiveRun;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BeginRun()
    {
        level = 1;
        elapsedTime = 0f;
        killCount = 0;
        isActiveRun = true;
    }

    public void ResetRun()
    {
        selectedCharacter = null;
        //selectedMap = null;
        //selectedMode = default;

        level = 0;
        elapsedTime = 0f;
        killCount = 0;
        isActiveRun = false;
    }
}

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

    public static System.Action<GameplayState> OnStateChange;
    private GameplayState currentState = GameplayState.Waiting;
    public GameplayState CurrentState => currentState;

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
        currentState = GameplayState.Waiting;
    }

    public void ChangeState(GameplayState newState)
    {
        currentState = newState;
        OnStateChange?.Invoke(newState);
    }    
}

public enum GameplayState
{
    Waiting,
    PlayerReady,
    MapReady,
    GameplayReady
}
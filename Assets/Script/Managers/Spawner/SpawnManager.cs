using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerSpawner playerSpawner;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        playerSpawner = gameObject.AddComponent<PlayerSpawner>();
        enemySpawner = gameObject.AddComponent<EnemySpawner>();
    }

    private void OnEnable() => PlayerSpawner.OnPlayerSpawned += OnPlayerReady;
    private void OnDisable() => PlayerSpawner.OnPlayerSpawned -= OnPlayerReady;

    private void OnPlayerReady(Transform transform)
    {
        //enemySpawner = gameObject.AddComponent<EnemySpawner>();  
    }

}

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private PlayerSpawner playerSpawner;
    private EnemySpawner enemySpawner;
    private ProjectileSpawner projectileSpawner;
    public ProjectileSpawner ProjectileSpawner => projectileSpawner;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerSpawner = gameObject.AddComponent<PlayerSpawner>();
        enemySpawner = gameObject.AddComponent<EnemySpawner>();
    }

    private void OnEnable() => PlayerSpawner.OnPlayerSpawned += OnPlayerReady;
    private void OnDisable() => PlayerSpawner.OnPlayerSpawned -= OnPlayerReady;

    private void OnPlayerReady(Transform transform)
    {
        projectileSpawner = gameObject.AddComponent<ProjectileSpawner>();
    }
}

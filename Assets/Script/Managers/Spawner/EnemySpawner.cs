using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    public Transform targetSpawn;

    [SerializeField]
    private float minimumSpawnRadius = 13f;

    [SerializeField]
    private float maximumSpawnRadius = 20f;

    private bool allowRespawn = true;

    // Function Spawn toàn bộ các enemy của các Pool enemy type
    // Function mở rộng một pool type nào đó với điều kiện theo thời gian
    // Function lấy vị trí ngẫu nhiên trong tầm minmax radius

    public void SetTarget(Transform newTarget)
    {
        targetSpawn = newTarget;
    }

    private void OnEnable()
    {
        PlayerSpawner.OnPlayerSpawned += SetTarget;
    }

    private void OnDisable()
    {
        PlayerSpawner.OnPlayerSpawned -= SetTarget;
    }

    private void Start()
    {
        if (targetSpawn == null)
            return;
        SpawnWave();
    }



    private void SpawnWave()
    {
        SpawnEnemy(EnemyType.Normal);
    }

    private void SpawnEnemy(EnemyType type)
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        GameObject obj = PoolManager.Instance.EnemyPools.Spawn(type, spawnPos, Quaternion.identity);
        Enemy enemy = obj.GetComponent<Enemy>();

        if (enemy == null)
            return;

        enemy.OnDeath += HandleEnemyDeath;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;

        float distance =
            Random.Range(
                minimumSpawnRadius,
                maximumSpawnRadius);

        Vector2 pos =
            (Vector2)targetSpawn.position +
            dir * distance;

        return pos;
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= HandleEnemyDeath;

        PoolManager.Instance.EnemyPools
            .Despawn(
                enemy.profile.enemyType,
                enemy.gameObject);

        if (!allowRespawn)
            return;

        SpawnEnemy(enemy.profile.enemyType);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform targetSpawn;

    private float minimumSpawnRadius = 13f;
    private float maximumSpawnRadius = 20f;

    private bool allowRespawn = true;
    
    // --- GAME 15 PHÚT & GIỚI HẠN CAP 100 ENEMY ---
    private const float GAME_DURATION = 900f; // 15 phút = 900 giây
    private const int MAX_TOTAL_ENEMIES = 100;
    private Dictionary<EnemyType, int> currentPoolSizes = new Dictionary<EnemyType, int>();
    private float lastWaveUpdateTime = 0f;
    private float waveInterval = 60f;

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

        foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType)))
        {
            currentPoolSizes[type] = PoolManager.Instance.EnemyPools.GetAll(type)?.Count ?? 0;
        }

        SpawnAllEnemy(EnemyType.Normal);
    }

    //Spawn all of type
    private void SpawnAllEnemy(EnemyType type)
    {
        List<GameObject> objects = PoolManager.Instance.EnemyPools.GetAllToUse(type);  

        for (int i = 0; i < objects.Count; i++)
        {
            GameObject obj = objects[i];
            obj.GetComponent<Enemy>().SetTarget(targetSpawn);
            obj.GetComponent<Enemy>().OnDeath += HandleEnemyDeath;
            Vector3 spawnPos = GetRandomSpawnPosition();
            obj.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
        }

        PoolManager.Instance.EnemyPools.SpawnAll(type, objects);
    }

    private void SpawnEnemy(EnemyType type, GameObject obj)
    {
        obj.GetComponent<Enemy>().SetTarget(targetSpawn);
        obj.GetComponent<Enemy>().OnDeath += HandleEnemyDeath;
        Vector3 spawnPos = GetRandomSpawnPosition();
        obj.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);

        PoolManager.Instance.EnemyPools.Spawn(type, obj);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;

        float distance = Random.Range(minimumSpawnRadius, maximumSpawnRadius);

        Vector2 pos = (Vector2)targetSpawn.position + dir * distance;

        return pos;
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= HandleEnemyDeath;

        PoolManager.Instance.EnemyPools.Despawn(enemy.profile.enemyType, enemy.gameObject);

        if (!allowRespawn)
            return;

        SpawnEnemy(enemy.profile.enemyType, enemy.gameObject);
    }

    private void Update()
    {
        if (GameplayTimer.Instance == null || targetSpawn == null) return;

        float currentTime = GameplayTimer.Instance.currentTime;

        // Kết thúc 15 phút game
        if (currentTime >= GAME_DURATION)
        {
            allowRespawn = false;
            return;
        }

        if (currentTime - lastWaveUpdateTime >= waveInterval)
        {
            lastWaveUpdateTime = currentTime;
            DifficultyUpdate(currentTime);
        }
    }

    /// <summary>
    /// (Timeline) 15 phút tăng số lượng enemy
    /// </summary>
    private void DifficultyUpdate(float time)
    {
        int totalCurrentCap = 0;
        foreach (var size in currentPoolSizes.Values)
        {
            totalCurrentCap += size;
        }

        if (totalCurrentCap >= MAX_TOTAL_ENEMIES) return;

        float minute = time / 60f;

        if (minute < 3)
        {
            TryExpandPool(EnemyType.Normal, 5, totalCurrentCap); //16
        }
        else if (minute >= 3f && minute < 4f)
        {
            SpawnAllEnemy(EnemyType.HighHP);                           
            TryExpandPool(EnemyType.Normal, 3, totalCurrentCap); 
        }
        else if (minute >= 4f && minute < 6f)
        {
            TryExpandPool(EnemyType.HighHP, 3, totalCurrentCap);
            TryExpandPool(EnemyType.Normal, 3, totalCurrentCap); 
        }
        else if (minute >= 6f && minute < 10f)
        {
            TryExpandPool(EnemyType.HighHP, 3, totalCurrentCap); 
            TryExpandPool(EnemyType.Normal, 3, totalCurrentCap); 
        }
        else if (minute >= 10f && minute < 11f)
        {
            SpawnAllEnemy(EnemyType.RunCross);
        }
        else if (minute >= 12f && minute < 13f)
        {
            SpawnAllEnemy(EnemyType.ShootDistance);
        }
        else
        {
            TryExpandPool(EnemyType.HighHP, 5, totalCurrentCap);
            TryExpandPool(EnemyType.RunCross, 5, totalCurrentCap);
            TryExpandPool(EnemyType.ShootDistance, 5, totalCurrentCap);
        }
    }

    private void TryExpandPool(EnemyType type, int amountToExpand, int totalCurrentCap)
    {
        if (totalCurrentCap + amountToExpand > MAX_TOTAL_ENEMIES)
        {
            amountToExpand = MAX_TOTAL_ENEMIES - totalCurrentCap;
        }

        if (amountToExpand <= 0) return;

        // Thực hiện lệnh gọi hàm Expand của hệ thống Pool bạn có sẵn
        PoolManager.Instance.EnemyPools.ExpandPool(type, amountToExpand);

        // Cập nhật dữ liệu size trong code Spawner
        currentPoolSizes[type] += amountToExpand;

        // Ngay lập tức thả số lượng quái vừa được mở rộng ra map
        SpawnAllEnemy(type);
    }
}

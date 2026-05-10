using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerSpawner playerSpawner;
    private EnemySpawner enemySpawner;

    private void Start()
    {
        playerSpawner = gameObject.AddComponent<PlayerSpawner>();
    }
}

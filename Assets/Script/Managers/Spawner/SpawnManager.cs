using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerSpawner playerSpawner;

    private void Start()
    {
        playerSpawner = gameObject.AddComponent<PlayerSpawner>();
    }
}

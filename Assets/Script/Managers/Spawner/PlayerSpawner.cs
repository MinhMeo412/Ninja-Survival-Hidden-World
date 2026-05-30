using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private CharacterProfile profile;
    public static Action<Transform> OnPlayerSpawned;

    private void Start()
    {
        profile = GameSession.Instance.selectedCharacter;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(profile.GetPrefab);

        player.GetComponent<Player>().OnSpawn(profile);

        OnPlayerSpawned?.Invoke(player.transform);
    }    
}

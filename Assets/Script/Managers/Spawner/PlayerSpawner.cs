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

        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = profile.worldSprite;
        }

        Animator anim = player.GetComponent<Animator>();
        if (anim != null)
        {
            anim.runtimeAnimatorController = profile.animatorController;
        }

        CharacterStats stats = player.GetComponent<CharacterStats>();
        if (stats != null)
        {
            stats.Init(profile);
        }

        OnPlayerSpawned?.Invoke(player.transform);
    }    
}

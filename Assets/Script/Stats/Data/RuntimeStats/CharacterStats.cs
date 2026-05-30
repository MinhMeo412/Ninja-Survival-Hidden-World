using System;
using UnityEngine;
using UnityEngine.U2D;

public class CharacterStats : MonoBehaviour
{
    public float maxHP { get; private set; }
    public float currentHP { get; private set; }
    public float damage { get; private set; }

    public float damageTriggerInterval { get; private set; }
    private float damageTriggerDelay = 0.5f;

    public static event Action<CharacterStats> OnPlayerHealthChanged;

    public void Init(CharacterProfile profile)
    {
        LoadFromProfile(profile);
    }

    public void LoadFromProfile(CharacterProfile profile)
    {
        maxHP = profile.stats.maxHP;
        currentHP = maxHP;
        damage = profile.stats.damage;

        OnHPChanged(0);

        MoveStats moveStats = gameObject.GetComponent<MoveStats>();
        moveStats.Init(profile.stats.moveSpeed);
    }

    private void Update()
    {
        if(damageTriggerInterval > 0)
            damageTriggerInterval -= Time.deltaTime;
        damageTriggerInterval = damageTriggerInterval <= 0 ? 0 : damageTriggerInterval;
    }

    public void OnHPChanged(float value)
    {
        damageTriggerInterval = damageTriggerDelay;
        currentHP += value;

        if(currentHP > maxHP)
            currentHP = maxHP;

        if (currentHP <= 0)
            Die();
        Debug.Log($"HP: {currentHP}");
        OnPlayerHealthChanged?.Invoke(this);
    }    

    private void Die()
    {
        Debug.Log($"Chết");
        GameManager.Instance.GameOver();
    }    
}

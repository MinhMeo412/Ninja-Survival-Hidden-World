using System;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public static PlayerLevelManager Instance;
    public int level { get; private set; }
    public float currentExp { get; private set; }
    public float maxExp { get; private set; }

    public static event Action<PlayerLevelManager> OnExpChanged;
    public static event Action OnLevelUp;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        level = 1;
        maxExp = 5;
        currentExp = 0;
        OnExpChanged?.Invoke(this);
    }

    public void IncreaseEXP(float value)
    {
        currentExp += value;
        if (currentExp >= maxExp)
        {
            level += 1;
            maxExp += (5 + (5*level));
            currentExp = 0;
            OnLevelUp?.Invoke();
        }

        OnExpChanged?.Invoke(this);
    }    
}

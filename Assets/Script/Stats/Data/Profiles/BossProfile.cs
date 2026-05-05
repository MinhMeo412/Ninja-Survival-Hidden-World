using UnityEngine;

[CreateAssetMenu(fileName = "BossProfile", menuName = "Game/Boss Profile")]
public class BossProfile : ScriptableObject
{
    public string bossName;
    public BaseStats stats;

    public float specialCooldown;
}
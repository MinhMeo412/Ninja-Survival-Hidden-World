using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileProfile", menuName = "Game/Projectile Profile")]
public class ProjectileProfile : Profile, IPrefabProvider
{
    [Header("Info")]
    public string projectileName;
    public bool belongToPlayer;

    [Header("Visual")]
    public Sprite worldSprite;
    //public RuntimeAnimatorController animatorController;

    [Header("Prefab")]
    [SerializeField]
    private GameObject prefabProjectile;
    public GameObject GetPrefab => prefabProjectile;

    [Header("Level Data (For Player)")]
    public List<WeaponLevelData> levelSettings;

    [Header("Stats")]
    public BaseStats stats;

    public WeaponLevelData GetLevelData(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, levelSettings.Count - 1);
        return levelSettings[index];
    }
}

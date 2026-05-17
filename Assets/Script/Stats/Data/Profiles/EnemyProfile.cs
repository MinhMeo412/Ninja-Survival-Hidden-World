using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Game/Enemy Profile")]
public class EnemyProfile : Profile, IPrefabProvider
{
    [Header("Info")]
    public string enemyName;
    public EnemyType enemyType;

    [Header("Visual")]
    public Sprite worldSprite;
    public RuntimeAnimatorController animatorController;

    [Header("Prefab")]
    [SerializeField]
    private GameObject prefabEnemy;
    public GameObject GetPrefab => prefabEnemy;

    [Header("Stats")]
    public BaseStats stats;
}
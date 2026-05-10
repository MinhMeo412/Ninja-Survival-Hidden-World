using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Game/Enemy Profile")]
public class EnemyProfile : Profile
{
    [Header("Info")]
    public string enemyName;
    public EnemyType enemyType;

    [Header("Visual")]
    public Sprite worldSprite;
    public RuntimeAnimatorController animatorController;

    [Header("Prefab Optional")]
    public GameObject prefab;

    [Header("Stats")]
    public BaseStats stats;
}
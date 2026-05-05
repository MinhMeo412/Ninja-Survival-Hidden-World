using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Game/Enemy Profile")]
public class EnemyProfile : ScriptableObject
{
    public string enemyName;
    public BaseStats stats;

    //public EnemyType type; // melee, ranged, boss...
}
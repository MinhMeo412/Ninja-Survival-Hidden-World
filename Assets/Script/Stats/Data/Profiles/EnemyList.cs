using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyGroup
{
    //Dễ sắp xếp 1 map đủ loại enemy
    public EnemyType type;
    public EnemyProfile profiles;
}

[CreateAssetMenu(fileName = "NewEnemyList", menuName = "Data/Enemy List")]
public class EnemyList : EntityList
{
    [Header("Danh sách Enemy theo Type")]
    public List<EnemyGroup> enemyGroups = new List<EnemyGroup>();
}
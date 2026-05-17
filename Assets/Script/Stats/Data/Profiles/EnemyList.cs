using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyGroup
{
    //Dễ sắp xếp 1 map đủ loại enemy
    [SerializeField, ReadOnly] private EnemyType type;
    public EnemyProfile profiles;

    public void SyncType()
    {
        if (profiles != null)
        {
            type = profiles.enemyType;
        }
    }
}

[CreateAssetMenu(fileName = "NewEnemyList", menuName = "Data/Enemy List")]
public class EnemyList : EntityList
{
    [Header("Danh sách Enemy")]
    public List<EnemyGroup> enemyGroups = new List<EnemyGroup>();

    private void OnValidate()
    {
        if (enemyGroups == null) return;

        foreach (var group in enemyGroups)
        {
            group.SyncType();
        }
    }
}
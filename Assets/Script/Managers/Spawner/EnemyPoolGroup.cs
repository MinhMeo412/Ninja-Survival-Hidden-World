using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolGroup
{
    private readonly Dictionary<EnemyType, ObjectPool<EnemyProfile>> enemyPools = new Dictionary<EnemyType, ObjectPool<EnemyProfile>>();

    public EnemyPoolGroup(EnemyList list, Transform root)
    {
        foreach (EnemyGroup group in list.enemyGroups)
        {
            EnemyProfile profile = group.profiles;

            if (group.profiles.enemyType == EnemyType.Normal)
            {
                enemyPools[group.profiles.enemyType] = new ObjectPool<EnemyProfile>(profile, 6, root);
            }
            else
            {
                enemyPools[group.profiles.enemyType] = new ObjectPool<EnemyProfile>(profile, 3, root);
            }
        }
    }
    
    public List<GameObject> GetAllToUse(EnemyType type)
    {
        List<GameObject> list = new List<GameObject>();
        if (!enemyPools.ContainsKey(type))
            return null;

        list = enemyPools[type].GetAllInactiveToUse();

        if (list == null) return null;

        return list;
    }

    public List<GameObject> GetAll(EnemyType type)
    {
        List<GameObject> list = new List<GameObject>();
        if (!enemyPools.ContainsKey(type))
            return null;

        list = enemyPools[type].GetAllInactive();

        if (list == null) return null;

        return list;
    }

    public void SpawnAll(EnemyType type, List<GameObject> list)
    {
        if (!enemyPools.ContainsKey(type))
            return;

        enemyPools[type].SetAllToActive(list);
    }

    public void Spawn(EnemyType type, GameObject obj)
    {
        if (!enemyPools.ContainsKey(type))
            return;

        enemyPools[type].SetActive(obj);
    }

    public void Despawn(EnemyType type, GameObject obj)
    {
        if (!enemyPools.ContainsKey(type))
            return;

        enemyPools[type].Return(obj);
    }

    public void ExpandPool(EnemyType type, int numbers)
    {
        if (!enemyPools.ContainsKey(type))
            return;

        enemyPools[type].Expand(numbers);
    }
}

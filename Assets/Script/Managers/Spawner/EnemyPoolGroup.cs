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

            enemyPools[group.profiles.enemyType] = new ObjectPool<EnemyProfile>(profile,10,10,root);
        }
    }
    
    public GameObject Spawn(EnemyType type, Vector3 pos, Quaternion rot)
    {
        if (!enemyPools.ContainsKey(type))
            return null;

        GameObject obj = enemyPools[type].Get();

        if (obj == null) return null;

        obj.transform.SetPositionAndRotation(pos,rot);

        return obj;
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

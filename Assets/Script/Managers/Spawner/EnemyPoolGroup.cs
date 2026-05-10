using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolGroup : MonoBehaviour
{
    private readonly Dictionary<EnemyType, ObjectPool> enemyPools = new Dictionary<EnemyType, ObjectPool>();

    public EnemyPoolGroup(EnemyList list, Transform root)
    {
        foreach (EnemyGroup group in list.enemyGroups)
        {
            EnemyProfile profile = group.profiles;

            enemyPools[group.type] = new ObjectPool(profile,10,100,root);
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

    public void ResizePool(EnemyType type, int maxSize)
    {
        if (!enemyPools.ContainsKey(type))
            return;

        enemyPools[type].SetMaxSize(maxSize);
    }
}

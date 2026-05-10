using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private class PoolItem
    {
        public GameObject gameObject;
        public IPoolable poolable;
    }

    private readonly Queue<PoolItem> inactiveObjects = new Queue<PoolItem>();

    private readonly Dictionary<GameObject, PoolItem> allItems = new Dictionary<GameObject, PoolItem>();

    private readonly GameObject prefab;

    private readonly Transform parent;

    private int currentSize;

    private int maxSize;

    public ObjectPool(Profile profile, 
                        int initialSize,
                        int maxSize,
                        Transform root)
    {
        EnemyProfile enemyProfile = profile as EnemyProfile;
        if (enemyProfile == null)
        {
            Debug.LogError("Profile is not EnemyProfile");
            return;
        }

        this.prefab = enemyProfile.prefab;
        this.maxSize = maxSize;

        parent = new GameObject(enemyProfile.name + "_Pool").transform;

        parent.SetParent(root);

        Expand(initialSize);
    }

    public void Expand(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            PoolItem item = InstantiateNew();
            inactiveObjects.Enqueue(item);
        }
    }

    public void SetMaxSize(int maxSize)
    {
        this.maxSize = maxSize;
    }

    private PoolItem InstantiateNew()
    {
        GameObject obj = Object.Instantiate(prefab, parent);
        obj.SetActive(false);

        PoolItem newItem = new PoolItem
        {
            gameObject = obj,
            poolable = obj.GetComponent<IPoolable>()
        };

        allItems.Add(obj, newItem);
        currentSize++;
        return newItem;
    }

    public GameObject Get()
    {
        PoolItem item = null;

        if (inactiveObjects.Count > 0)
        {
            item = inactiveObjects.Dequeue();
        }
        else if (currentSize < maxSize)
        {
            item = InstantiateNew();
        }

        if (item == null)
        {
            Debug.LogWarning("Pool already maxed!");
            return null;
        }

        item.gameObject.SetActive(true);
        item.poolable?.OnSpawn();

        return item.gameObject;
    }

    public void Return(GameObject obj)
    {
        if (allItems.TryGetValue(obj, out PoolItem item))
        {
            item.poolable?.OnDespawn();
            item.gameObject.SetActive(false);
            inactiveObjects.Enqueue(item);
        }
        else
        {
            Debug.LogWarning("Object not belong to this Pool!");
        }
    }
}

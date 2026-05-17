using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ObjectPool<T> where T : Profile, IPrefabProvider
{
    private class PoolItem
    {
        public GameObject gameObject;
        public IPoolable<T> poolable;
    }

    private readonly Queue<PoolItem> inactiveObjects = new Queue<PoolItem>();

    private readonly Dictionary<GameObject, PoolItem> allItems = new Dictionary<GameObject, PoolItem>();

    private readonly T profile;

    private readonly Transform parent;

    public ObjectPool(T profile, 
                        int initialSize,
                        int maxSize,
                        Transform root)
    {
        this.profile = profile;
        if (profile == null)
        {
            Debug.LogError("Profile is null");
            return;
        }

        parent = new GameObject(profile.name + "_Pool").transform;

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

    private PoolItem InstantiateNew()
    {
        GameObject obj = Object.Instantiate(profile.GetPrefab, parent);
        obj.SetActive(false);

        PoolItem newItem = new PoolItem
        {
            gameObject = obj,
            poolable = obj.GetComponent<IPoolable<T>>()
        };

        allItems.Add(obj, newItem);
        return newItem;
    }

    public GameObject Get()
    {
        PoolItem item = null;

        if (inactiveObjects.Count > 0)
        {
            item = inactiveObjects.Dequeue();
        }

        if (item == null)
        {
            Debug.LogWarning("Pool already maxed!");
            return null;
        }

        item.gameObject.SetActive(true);
        item.poolable?.OnSpawn(profile);

        return item.gameObject;
    }

    public void GetAllInactive()
    {
        PoolItem item = null;
        for (int i = 0;i < inactiveObjects.Count;i++)
        {
            item = inactiveObjects.Dequeue();
            item.gameObject.SetActive(true);
            item.poolable?.OnSpawn(profile);
        }    
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

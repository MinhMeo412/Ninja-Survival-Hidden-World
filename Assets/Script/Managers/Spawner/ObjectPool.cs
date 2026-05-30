using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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

    public List<GameObject> GetAllInactiveToUse()
    {
        List<GameObject> gameObjects = new List<GameObject>();

        int count = inactiveObjects.Count;

        for (int i = 0; i < count; i++)
        {
            PoolItem item = inactiveObjects.Dequeue();
            gameObjects.Add(item.gameObject);
        }

        return gameObjects;
    }

    public List<GameObject> GetAllInactive()
    {
        List<GameObject> gameObjects = new List<GameObject>();

        int count = inactiveObjects.Count;

        foreach (PoolItem item in inactiveObjects)
        {
            gameObjects.Add(item.gameObject);
        }

        return gameObjects;
    }

    public GameObject GetInactiveToUse()
    {
        if (inactiveObjects.Count == 0)
            { return null; }
        GameObject gameObjects = inactiveObjects.Dequeue().gameObject; 

        return gameObjects;
    }


    public void SetAllToActive(List<GameObject> gameObjects)
    {
        PoolItem item = null;
        for (int i = 0;i < gameObjects.Count;i++)
        {
            if(allItems.TryGetValue(gameObjects[i].gameObject, out item))
            {
                gameObjects[i].SetActive(true);
                item.poolable?.OnSpawn(profile);
            }    
        }    
    }

    public void SetActive(GameObject gameObject)
    {
        PoolItem item = null;

        if (allItems.TryGetValue(gameObject, out item))
        {
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

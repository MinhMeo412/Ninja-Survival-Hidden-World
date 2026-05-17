using UnityEngine;

public interface IPoolable<T>
{
    public void OnSpawn(T profile);
    public void OnDespawn();
}

using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable<EnemyProfile>
{
    public EnemyProfile profile { get; private set; }

    private bool initialized;

    public System.Action<Enemy> OnDeath;

    public void OnSpawn(EnemyProfile profile) 
    {
        this.profile = profile;

        if (!initialized)
        {
            //Cài các sprite, animator trong first spawn
            initialized = true;
        }

        Activate();
    }

    public void OnDespawn()
    {

    }

    private void Activate()
    {

    }

    public void Die()
    {
        OnDeath?.Invoke(this);
    }
}

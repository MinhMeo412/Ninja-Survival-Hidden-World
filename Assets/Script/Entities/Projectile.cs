using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable<ProjectileProfile>
{
    public ProjectileProfile profile { get; private set; }

    public Transform target;
    private ProjectileInputHandler inputHandler;
    private ProjectileStats stats;

    private SpriteRenderer sr;

    private bool initialized = false;

    public void OnSpawn(ProjectileProfile profile)
    {
        if (!initialized)
        {
            this.profile  = profile;

            sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = profile.worldSprite;
            }

            //Animator anim = gameObject.GetComponent<Animator>();
            //if (anim != null)
            //{
            //    anim.runtimeAnimatorController = profile.animatorController;
            //}

            stats = gameObject.GetComponent<ProjectileStats>();
            if (stats != null)
            {
                stats.Init(profile);
            }

            inputHandler = gameObject.GetComponent<ProjectileInputHandler>();

            initialized = true;
        }

        if(PlayerWeaponInventory.Instance != null && PlayerWeaponInventory.Instance.OwnedWeapons.TryGetValue(profile, out int level))
        {
            stats.UpgradeWP(profile, level);
        }      
    }

    public void Launch(Vector3 spawnPosition, Transform target)
    {
        transform.position = spawnPosition;

        if (inputHandler != null)
        {
            inputHandler.Init(profile, target);
        }
    }

    public void OnDespawn()
    {
    }

    public void OnHit()
    {
        PoolManager.Instance.ProjectilePools.ReturnProjectile(profile, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
        {
            Debug.Log("dính enemy");
            OnHit();
        }

    }
}

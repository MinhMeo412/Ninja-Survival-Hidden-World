using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public void SpawnProjectile(ProjectileProfile profile, Vector3 spawnPosition, Transform target)
    {
        if (profile == null) return;

        Projectile projectile = PoolManager.Instance.ProjectilePools.GetProjectile(profile);

        if (projectile != null)
        {
            projectile.Launch(spawnPosition, target);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolGroup
{
    private readonly Dictionary<ProjectileProfile, ObjectPool<ProjectileProfile>> projectilePools = new Dictionary<ProjectileProfile, ObjectPool<ProjectileProfile>>();

    public ProjectilePoolGroup (ProjectileList list, Transform root)
    {
        GameObject fromEnemy = new GameObject("From Enemy");
        GameObject fromPlayer = new GameObject("From Player");

        fromEnemy.transform.SetParent(root);
        fromPlayer.transform.SetParent(root);

        foreach (ProjectileProfile profile in list.profiles)
        {
            if (projectilePools.ContainsKey(profile)) continue;

            Transform parent = profile.belongToPlayer? fromPlayer.transform : fromEnemy.transform;

            projectilePools[profile] = new ObjectPool<ProjectileProfile> (profile, 1, parent);
        }
    }

    public Projectile GetProjectile(ProjectileProfile profile)
    {
        if (!projectilePools.TryGetValue(profile, out var pool))
        {
            Debug.LogError($"Pool không tồn tại cho profile: {profile.name}");
            return null;
        }

        GameObject projectileObj = pool.GetInactiveToUse();

        if (projectileObj == null)
        {
            pool.Expand(1); // Tự mở rộng
            projectileObj = pool.GetInactiveToUse();
        }

        pool.SetActive(projectileObj);

        return projectileObj.GetComponent<Projectile>();
    }

    public void ReturnProjectile(ProjectileProfile profile, GameObject obj)
    {
        if (projectilePools.TryGetValue(profile, out var pool))
        {
            pool.Return(obj);
        }
    }
}

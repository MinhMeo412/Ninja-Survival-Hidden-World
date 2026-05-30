using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    private ProjectileProfile profile;
    private int currentLevel;
    private WeaponLevelData currentData;
    private float cooldownTimer;

    public void Init(ProjectileProfile profile, int level)
    {
        this.profile = profile;
        UpdateLevel(level);
    }

    public bool UpdateLevel(int newLevel)
    {
        if (newLevel > profile.levelSettings.Count)
        {
            Debug.LogWarning("Weapon đã đạt cấp độ tối đa!");
            return false;
        }

        currentLevel = newLevel;
        currentData = profile.GetLevelData(newLevel);
        return true;
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            ExecuteAttack();
            cooldownTimer = currentData.fireRate;
        }
    }

    private void ExecuteAttack()
    {
        List<Enemy> targets = TargetingSystem.GetNearestTargets(transform.position, 7, currentData.projectileCount);

        if (targets.Count == 0) return;

        if (targets.Count < currentData.projectileCount)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                Transform targetTransform = targets[i].transform;

                SpawnManager.Instance.ProjectileSpawner.SpawnProjectile(profile, transform.position, targetTransform);
            }
            cooldownTimer = currentData.fireRate;
        }
        else
        {
            for (int i = 0; i < currentData.projectileCount; i++)
            {
                Transform targetTransform = targets[i].transform;

                SpawnManager.Instance.ProjectileSpawner.SpawnProjectile(profile, transform.position, targetTransform);
            }
            cooldownTimer = currentData.fireRate;
        }    
    }
}

using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public float damage { get; private set; }

    public void Init(ProjectileProfile profile)
    {
        LoadFromProfile(profile);
    }

    public void LoadFromProfile(ProjectileProfile profile)
    {
        damage = profile.stats.damage + profile.GetLevelData(1).damageIncrease;

        MoveStats moveStats = gameObject.GetComponent<MoveStats>();
        moveStats.Init(profile.stats.moveSpeed);
    }

    public void UpgradeWP(ProjectileProfile profile, int level)
    {
        damage = profile.stats.damage + profile.GetLevelData(level).damageIncrease;
    }    
}

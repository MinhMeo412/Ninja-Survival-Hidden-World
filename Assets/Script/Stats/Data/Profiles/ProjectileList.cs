using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileList", menuName = "Data/Projectile List")]
public class ProjectileList : EntityList
{
    [Header("Danh sách Projectile")]
    public List<ProjectileProfile> profiles;
}
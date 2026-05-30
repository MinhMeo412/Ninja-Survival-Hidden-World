using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [Header("Enemy")]
    [SerializeField]
    private EnemyList enemyList;

    [Header("Projectile")]
    [SerializeField]
    private ProjectileList projectileList;
    public ProjectileList ProjectileList => projectileList;

    [Header("Items")]
    //[SerializeField]
    //private GameObject expPrefab;

    public EnemyPoolGroup EnemyPools
    { get; private set; }
    public ProjectilePoolGroup ProjectilePools
        { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //enemyList = GameSession.Instance.enemyList;

        //Tạo container chứa enemy pool
        GameObject enemyPoolsRoot = new GameObject("EnemyPools");
        //Tạo các pool enemy 
        EnemyPools = new EnemyPoolGroup(enemyList, enemyPoolsRoot.transform);

        GameObject projectileRoot = new GameObject("ProjectilePool");
        ProjectilePools = new ProjectilePoolGroup(projectileList, projectileRoot.transform);

        //ItemPools =
        //    new ItemPoolGroup(
        //        expPrefab,
        //        goldPrefab,
        //        transform);
    }
}

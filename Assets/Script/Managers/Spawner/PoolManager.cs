using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [Header("Enemy")]
    [SerializeField]
    private EnemyList enemyList;

    [Header("Projectile")]
    //[SerializeField]
    //private ProjectileProfileList projectileList;

    [Header("Items")]
    //[SerializeField]
    //private GameObject expPrefab;

    public EnemyPoolGroup EnemyPools
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

        EnemyPools =
            new EnemyPoolGroup(
                enemyList,
                transform);

        //ProjectilePools =
        //    new ProjectilePoolGroup(
        //        projectileList,
        //        transform);

        //ItemPools =
        //    new ItemPoolGroup(
        //        expPrefab,
        //        goldPrefab,
        //        transform);
    }
}

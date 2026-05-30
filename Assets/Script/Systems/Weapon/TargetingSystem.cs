using System.Collections.Generic;
using UnityEngine;

public static class TargetingSystem
{
    private static readonly Collider2D[] scanResults = new Collider2D[130];
    private static readonly List<Enemy> validTargets = new();
    private static readonly List<Enemy> result = new();

    private static ContactFilter2D enemyFilter;
    private static bool isFilterInitialized = false;
    private static void InitializeFilter()
    {
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
        enemyFilter.useLayerMask = true;

        // Tắt quét các Trigger (chỉ quét collider đặc) để tối ưu thêm:
        // enemyFilter.useTriggers = false;

        isFilterInitialized = true;
    }

    public static List<Enemy> GetNearestTargets(Vector3 origin, float range, int count)
    {
        validTargets.Clear();

        if (!isFilterInitialized)
        {
            //Debug.Log("[GetNearestTargets] Bộ lọc chưa khởi tạo. Đang tiến hành Khởi tạo Filter...");
            InitializeFilter();
        }

        int hitCount = Physics2D.defaultPhysicsScene.OverlapCircle(
            origin,
            range,
            enemyFilter,
            scanResults
        );

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D col = scanResults[i];

            if (col.TryGetComponent<Enemy>(out var enemy))
            {
                if (enemy.gameObject.activeInHierarchy)
                {
                    validTargets.Add(enemy);
                }
            }
        }

        // Sắp xếp theo khoảng cách
        validTargets.Sort((a, b) => (a.transform.position - origin).sqrMagnitude.CompareTo((b.transform.position - origin).sqrMagnitude));

        result.Clear();

        int finalCount = Mathf.Min(count, validTargets.Count);

        for (int i = 0; i < finalCount; i++)
        {
            result.Add(validTargets[i]);
        }

        return result;
    }
}


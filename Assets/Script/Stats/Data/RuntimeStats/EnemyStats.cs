using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHP { get; private set; }
    public float currentHP { get; private set; }
    public float damage { get; private set; }

    public void Init(EnemyProfile profile)
    {
        LoadFromProfile(profile);
    }

    public void LoadFromProfile(EnemyProfile profile)
    {
        maxHP = profile.stats.maxHP;
        currentHP = maxHP;
        damage = profile.stats.damage;

        MoveStats moveStats = gameObject.GetComponent<MoveStats>();
        moveStats.Init(profile.stats.moveSpeed);
    }

    private void Update()
    {
    }

    public void OnHPChanged(float value)
    {
        currentHP += value;
        Debug.Log($"HP: {currentHP}");
        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.GetComponent<Enemy>().Die();
        currentHP = maxHP;
    }
}

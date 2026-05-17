using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHP { get; private set; }
    public float currentHP { get; private set; }
    public float moveSpeed { get; private set; }
    public float damage { get; private set; }

    public void Init(CharacterProfile profile)
    {
        LoadFromProfile(profile);
    }

    public void LoadFromProfile(CharacterProfile profile)
    {
        maxHP = profile.stats.maxHP;
        currentHP = maxHP;
        damage = profile.stats.damage;

        MoveStats moveStats = gameObject.GetComponent<MoveStats>();
        moveStats.Init(profile.stats.moveSpeed);
    }
}

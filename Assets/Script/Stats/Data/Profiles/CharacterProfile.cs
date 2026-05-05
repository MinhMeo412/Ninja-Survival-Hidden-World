using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "Game/Character Profile")]
public class CharacterProfile : ScriptableObject
{
    [Header("Info")]
    public string characterName;
    public Sprite icon;

    [Header("Visual")]
    public Sprite worldSprite;
    public RuntimeAnimatorController animatorController;

    [Header("Prefab Optional")]
    public GameObject prefab;

    [Header("Stats")]
    public BaseStats stats;
    public float pickupRange;
    public float attackSpeed;
}
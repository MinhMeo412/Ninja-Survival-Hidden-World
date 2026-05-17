using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "Game/Character Profile")]
public class CharacterProfile : Profile, IPrefabProvider
{
    [Header("Info")]
    public string characterName;
    public Sprite icon;

    [Header("Visual")]
    public Sprite worldSprite;
    public RuntimeAnimatorController animatorController;

    [Header("Prefab Optional")]
    [SerializeField]
    private GameObject prefabPlayer;
    public GameObject GetPrefab => prefabPlayer;

    [Header("Stats")]
    public BaseStats stats;
    public float pickupRange;
    public float attackSpeed;
}
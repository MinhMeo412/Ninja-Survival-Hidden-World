using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCharacterList", menuName = "Data/Character List")]
public class CharacterList : EntityList
{
    [SerializeField]
    public List<CharacterProfile> profiles = new List<CharacterProfile>();
}

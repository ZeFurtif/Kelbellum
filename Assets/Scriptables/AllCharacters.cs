using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Static Character Data", menuName = "Kelbellum/Static Character Data")]
public class AllCharacters : ScriptableObject
{

    [Header("Characters Settings")]
    public List<StaticCharacterData> charactersData = new List<StaticCharacterData>();

}

[System.Serializable]
public class StaticCharacterData
{
    [Header("Character Identity")]
    public string characterName;
    public Sprite characterSprite;
    public string oneLiner;
    
    [Header("Character Stats")]
    public CharacterData characterSO;
}

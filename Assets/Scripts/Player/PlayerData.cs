using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public CharacterData currentCharacter;
    
    public PrimaryData currentPrimaryData;
    public SecondaryData currentSecondaryData;

    [Header("Current Player Stats")]
    public float health;
    public float speed;
    
    public float strength;
    public float dexterity;
    public float arcana;

    public float strResistance;
    public float dexResistance;
    public float arcResistance;

    public float critChance;
    public float lifeSteal;

    void OnEnable()
    {
        CopyCharacterStats();
    }

    void CopyCharacterStats()
    {
        if (currentCharacter) //COPY ALL STATS TO PLAYER SO THE DATA USEABLE
        {
            health = currentCharacter.health;
            speed = currentCharacter.speed;
            
            strength = currentCharacter.strength;
            dexterity = currentCharacter.dexterity;
            arcana = currentCharacter.arcana;

            strResistance = currentCharacter.strResistance;
            dexResistance = currentCharacter.dexResistance;
            arcResistance = currentCharacter.arcResistance;

            critChance = currentCharacter.critChance;
            lifeSteal = currentCharacter.lifeSteal;
        }
    }

    public void LoadPlayerData(CharacterData characterData)
    {
        currentCharacter = characterData;
        CopyCharacterStats();
    }

}

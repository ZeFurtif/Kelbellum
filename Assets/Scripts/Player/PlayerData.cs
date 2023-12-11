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

    public float size;
    public float cooldownAcceleration;
    public float critChance;
    public float lifeSteal;
    public float evasiveness;
    public float regeneration;

    void Awake()
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

            size = currentCharacter.size;
            cooldownAcceleration = currentCharacter.cooldownAcceleration;
            critChance = currentCharacter.critChance;
            lifeSteal = currentCharacter.lifeSteal;
            evasiveness = currentCharacter.evasiveness;
            regeneration = currentCharacter.regeneration;
        }
    }

    public void LoadPlayerData(CharacterData characterData)
    {
        currentCharacter = characterData;
        CopyCharacterStats();
    }

    public void LoadPrimaryData(PrimaryData primaryData)
    {
        currentPrimaryData = primaryData;
    }

    public void AddStats(CharacterData charData)
    {
        speed += charData.speed;
        strength += charData.strength;
        dexterity += charData.dexterity;
        arcana += charData.arcana;
        strResistance += charData.strResistance;
        dexResistance += charData.dexResistance;
        arcResistance += charData.arcResistance;
        size += charData.size;
        cooldownAcceleration += charData.cooldownAcceleration;
        critChance += charData.critChance;
        lifeSteal += charData.lifeSteal;
        evasiveness += charData.evasiveness;
        regeneration += charData.regeneration;
    }

    public void SubstractStats(CharacterData charData)
    {
        speed -= charData.speed;
        strength -= charData.strength;
        dexterity -= charData.dexterity;
        arcana -= charData.arcana;
        strResistance -= charData.strResistance;
        dexResistance -= charData.dexResistance;
        arcResistance -= charData.arcResistance;
        size -= charData.size;
        cooldownAcceleration -= charData.cooldownAcceleration;
        critChance -= charData.critChance;
        lifeSteal -= charData.lifeSteal;
        evasiveness -= charData.evasiveness;
        regeneration -= charData.regeneration;
    }

}

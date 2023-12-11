using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Secondary Data", menuName = "Kelbellum/Secondary Data")]
public class UpgradeData : ScriptableObject
{
    
    public new string name; 

    public enum UpgradeLevel
    {
        copper,
        silver,
        gold,
        platinum
    }

    public enum UpgradeType
    {
        stats,
        heal,
        element
    }

    public UpgradeLevel upgradeLevel;
    public UpgradeType upgradeType;

    [Header("Stats Up")]
    public CharacterData statsUpData;

    [Header("Heal")]
    public float healAmount;

}

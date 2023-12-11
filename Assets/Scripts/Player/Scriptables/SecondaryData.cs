using UnityEngine;

[CreateAssetMenu(fileName = "New Secondary Data", menuName = "Kelbellum/Secondary Data")]
public class SecondaryData : ScriptableObject
{
    
    public new string name; 

    public enum SecondaryType
    {
        melee,
        shield,
        heal,
        statsUp,
        areaEffect
    }

    [Header("Primary Ability")]
    public SecondaryType type;
    public float cooldown;
    
    [Header("Prefabs & FX")]
    public GameObject[] fxPrefabs;

    [Header("Damage")]
    public float forceDamage;
    public float precisionDamage;
    public float magicDamage;

    [Header("Melee")]
    public float range;
    public float knockback;
    public float playerRecoil;

    [Header("Shield")]
    public int shieldValue;
    public int shieldDuration;

    [Header("Heal")]
    public int healValue;

    [Header("Stats Up")]
    public CharacterData statsUpData;
    public float statsUpDuration;
    
    [Header("Area of Effect")]
    public GameObject areaObject;
}
using UnityEngine;

[CreateAssetMenu(fileName = "New Secondary Data", menuName = "Kelbellum/Secondary Data")]
public class SecondaryData : ScriptableObject
{
    
    public enum SecondaryType
    {
        shield,
        heal,
        statsUp
    }

    [Header("Primary Ability")]
    public SecondaryType type;

    public float baseValue;
    [HideInInspector] public float value;

    public float baseCooldown;
    [HideInInspector] public float cooldown;


    [Header("Prefabs & FX")]
    public GameObject fxPrefab;
    public GameObject additionalPrefab;
    
}
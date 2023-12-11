using UnityEngine;

[CreateAssetMenu(fileName = "New Primary Data", menuName = "Kelbellum/Primary Data")]
public class PrimaryData : ScriptableObject
{
    
    public new string name; 

    public enum PrimaryType
    {
        melee,
        range,
        spell
    }

    [Header("Settings")]
    public PrimaryType type;

    public float forceDamage;
    public float precisionDamage;
    public float magicDamage;
    [Space(5)]
    public float range;
    public float knockback;
    public float attackSpeed;
    [Space(5)]
    public bool useComboSystem;
    public int comboMax;
    public float comboWindow;

    [Header("Melee")]
    public GameObject meleeFX;
    public Vector3[] FXRotations;

    [Header("Range")]
    public float projectileSpeed;
    public GameObject projectilePrefab;

    [Header("Spell")]
    public GameObject spellPrefab;
    
}
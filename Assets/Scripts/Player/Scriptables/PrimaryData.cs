using UnityEngine;

[CreateAssetMenu(fileName = "New Primary Data", menuName = "Kelbellum/Primary Data")]
public class PrimaryData : ScriptableObject
{
    
    public enum PrimaryType
    {
        melee,
        range,
        spell
    }

    [Header("Primary Ability")]
    public PrimaryType type;

    public float baseDamage;
    [HideInInspector] public float damage;

    public float baseRange;
    [HideInInspector] public float range;

    public float baseKnockback;
    [HideInInspector] public float knockback;

    public float baseAttackSpeed;
    [HideInInspector] public float attackSpeed;


    [Header("Prefabs & FX")]
    public float projectileSpeed;
    public GameObject projectilePrefab;
    
}
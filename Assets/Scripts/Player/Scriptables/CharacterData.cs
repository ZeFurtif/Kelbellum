using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Kelbellum/Character Data")]
public class CharacterData : ScriptableObject
{

    public new string name;
    
    [Header("Root Stats")]
    public float health = 50;
    public float speed = 3;
    public float attackSpeed = 1;
    
    public float strength = 1;
    public float dexterity = 1;
    public float arcana = 1;

    public float strResistance = 1;
    public float dexResistance = 1;
    public float arcResistance = 1;

    [Header("Discrete Stats")]
    public float size = 1;
    public float cooldownAcceleration = 0;
    public float critChance = 0;
    public float lifeSteal = 0;
    public float evasiveness = 0;
    public float regeneration = 0;

    [Header("Abilities")]
    public PrimaryData[] primaries;
    public SecondaryData[] secondaries;

}

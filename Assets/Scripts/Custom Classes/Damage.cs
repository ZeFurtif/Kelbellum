using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public Vector3 damageVector;
    public GameObject attacker;

    public Damage(Vector3 damageVector, GameObject attacker)
    {
        this.damageVector = damageVector;
        this.attacker = attacker;
    }
}
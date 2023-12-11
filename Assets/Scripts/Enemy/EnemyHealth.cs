using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 15f;
    public GameObject damageDisplay;
    
    public void ApplyDamage(Damage damage)
    {
        DisplayDamage(damage.damageVector);
        float cumulativeDamage = DamageAfterResistances(damage.damageVector);
        health -= cumulativeDamage;
        if (health <= 0)
        {
            Die();
        }
    }

    public float DamageAfterResistances(Vector3 damage)
    {
        return (damage.x / 1 + damage.y / 1 + damage.z / 1);
    }

    public void DisplayDamage(Vector3 damage)
    {
        GameObject display = Instantiate(damageDisplay, transform.position + new Vector3(0,1f,-1f), Quaternion.identity);
        display.transform.GetChild(0).SendMessage("ModifyDisplay", damage);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
}

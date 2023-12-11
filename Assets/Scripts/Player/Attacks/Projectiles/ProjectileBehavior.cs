using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float destroyDelay = 8f;

    public bool destroyOnInstantiate = true;
    public bool destroyOnHit = true;

    private Rigidbody rb;

    public Damage damageToCarry;
    public float knockbackAmount;

    void Start()
    {
        if (destroyOnInstantiate) {
            DestroyOnTime();
        }

        rb = GetComponent<Rigidbody>();
    }

    public void DestroyOnTime()
    {
        Destroy(gameObject, destroyDelay);
    }

    public void CarryDamage(Damage damage)
    {
        damageToCarry = damage;
    }

    public void CarryKnockback(float knockback)
    {
        knockbackAmount = knockback;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("GetKnockback", knockbackAmount * Vector3.Normalize(rb.velocity));
            collision.gameObject.SendMessage("ApplyDamage", damageToCarry);
            Destroy(gameObject, 0f);
        }
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerPrefab" && collision.gameObject.tag != "PlayerObjects")
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }


}

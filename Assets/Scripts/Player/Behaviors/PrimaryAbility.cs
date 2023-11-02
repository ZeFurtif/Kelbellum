using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PrimaryAbility : MonoBehaviour
{
    private PlayerData playerData;
    private PrimaryData primaryData;

    public bool canUsePrimary;

    private float cooldown;
    private float lastAttackTime = 0;
        
    private GameObject[] enemies;
    private GameObject[] enemiesHit;
    private GameObject closestEnemy;

    Camera cam;

    void Start()
    {
        playerData = gameObject.GetComponent<PlayerData>();
        primaryData = playerData.currentPrimaryData;
        canUsePrimary = primaryData;

        if (canUsePrimary)
        {
            cam = Camera.main.GetComponent<Camera>();
        }
    }    

    public void LoadPlayerData(PlayerData newData)
    {
        playerData = newData;
    }

    public void LoadPrimarydata(PrimaryData newprimaryData)
    {
        primaryData = newprimaryData;
    }

    // ABILITY LOGIC

    public void OnPrimary(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canUsePrimary && CooldownCheck())
        {
            UpdateEnemyList();
            switch (primaryData.type)
            {
                case PrimaryData.PrimaryType.melee:
                    MeleeEnemyCheck();
                    Hit();
                    break;
                
                case PrimaryData.PrimaryType.range:
                    RangeEnemyCheck();
                    Fire();
                    break;

                case PrimaryData.PrimaryType.spell:
                    Debug.Log("Spell");
                    break;
            }
            lastAttackTime = Time.time;
        }
    }

    private bool CooldownCheck()
    {
        if (Time.time - lastAttackTime < 1/primaryData.attackSpeed )
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // DAMAGE SHENANIGANS

    private float CalculateDamage()
    {
        return primaryData.damage * GetDamageMultiplier() * DoesItCrit();
    }

    private float GetDamageMultiplier()
    {
        switch (primaryData.type)
        {
            case PrimaryData.PrimaryType.melee:
                return playerData.strength;
            
            case PrimaryData.PrimaryType.range:
                return playerData.dexterity;

            case PrimaryData.PrimaryType.spell:
                return playerData.arcana;
        }
        return 1;
    }

    private float DoesItCrit()
    {
        {
            if (Random.value <= (playerData.critChance/100))
            {
                return 1.5f;
            }
            return 1;
        }
    }

    // ATTACK LOGIC : ARE YOU HITTING??

    private void UpdateEnemyList()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void MeleeEnemyCheck()
    {
        List<GameObject> enemiesInRange = new List<GameObject>();
        foreach (GameObject g in enemies)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
            float dotProduct = Vector3.Dot(g.transform.position - this.gameObject.transform.position, transform.rotation * Vector3.forward);


            if (dist < primaryData.range && dotProduct > 0.5f)
            {
                enemiesInRange.Add(g);
            }
        }
        enemiesHit = enemiesInRange.ToArray();
    }

    private void RangeEnemyCheck()
    {
        float oldDistance = primaryData.range;
        foreach (GameObject g in enemies)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
            if (dist < oldDistance)
            {
                closestEnemy = g;   
                oldDistance = dist;
            }

            if (closestEnemy != null)
            {
                Vector3 viewPos = cam.WorldToViewportPoint(closestEnemy.transform.position);
                if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
                {
                }
                else
                {
                    closestEnemy = null;
                }
            }
        }
    }

    private void Hit()
    {
        if (enemiesHit != null)
        {
            foreach (GameObject g in enemiesHit)
            {
                g.SendMessage("ApplyDamage", CalculateDamage());

                Vector3 knockbackVector = Vector3.Normalize(g.transform.position - this.gameObject.transform.position);
                g.SendMessage("GetKnockback", primaryData.knockback * knockbackVector);
            }
        }
    }

    private void Fire()
    {
        GameObject shot = Instantiate(primaryData.projectilePrefab, transform.position + (transform.rotation * Vector3.forward) , transform.rotation);
        Rigidbody rb = shot.GetComponent<Rigidbody>();
        Vector3 aimVector = (transform.rotation * Vector3.forward);
        if (closestEnemy)
        {
            Rigidbody closestRb = closestEnemy.GetComponent<Rigidbody>();
            Vector3 enemyVector = Vector3.Normalize(closestEnemy.transform.position - (transform.position + transform.rotation * Vector3.forward));
            if (Vector3.Dot(enemyVector, aimVector) > 0.7f)
            {
                rb.velocity = enemyVector * primaryData.projectileSpeed;
            }   
            else
            { 
                rb.velocity = aimVector * primaryData.projectileSpeed;
            }
        }
        else
        {
            rb.velocity = aimVector * primaryData.projectileSpeed;
        }

        shot.transform.rotation = Quaternion.LookRotation(rb.velocity);
        shot.SendMessage("CarryDamage", CalculateDamage());
    }

}
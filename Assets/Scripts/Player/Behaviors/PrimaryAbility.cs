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
    private int currentComboCount = 0;

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
            if (primaryData.useComboSystem)
            {
                UpdateComboState();
                //Debug.Log(currentComboCount);
            }
            UpdateEnemyList();
            switch (primaryData.type)
            {
                case PrimaryData.PrimaryType.melee:
                    MeleeFX();
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

    // COMBO STUFF

    private void UpdateComboState()
    {
        if (Time.time - lastAttackTime <= primaryData.comboWindow)
        {
            currentComboCount += 1;
        }
        else 
        {
            currentComboCount = 0;
        }
        currentComboCount = currentComboCount % primaryData.comboMax;
    }

    // DAMAGE SHENANIGANS

    private Vector3 CalculateDamage()
    {
        return new Vector3(primaryData.forceDamage * playerData.strength, primaryData.precisionDamage * playerData.dexterity, primaryData.magicDamage * playerData.arcana)* DoesItCrit();
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

        // ENEMY CHECKS

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

        // ENEMY HITS

    private void Hit()
    {
        if (enemiesHit != null)
        {
            foreach (GameObject g in enemiesHit)
            {
                Vector3 knockbackVector = Vector3.Normalize(g.transform.position - this.gameObject.transform.position);
                g.SendMessage("GetKnockback", primaryData.knockback * knockbackVector);
                g.SendMessage("ApplyDamage", new Damage(CalculateDamage(), this.gameObject ));
            }
        }
    }

    private void Fire()
    {
        GameObject shot = Instantiate(primaryData.projectilePrefab, transform.position + new Vector3(0, 1f, 0) + (transform.rotation * Vector3.forward) , transform.rotation);
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
        shot.SendMessage("CarryDamage", new Damage(CalculateDamage(), this.gameObject ));
        shot.SendMessage("CarryKnockback", primaryData.knockback);
    }

        // FX

    private void MeleeFX()
    {
        if (primaryData.meleeFX)
        {
            GameObject newlyInstantiatedFX = Instantiate(primaryData.meleeFX, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            newlyInstantiatedFX.transform.Rotate(primaryData.FXRotations[currentComboCount%primaryData.FXRotations.Length] + transform.eulerAngles, Space.World);
            Destroy(newlyInstantiatedFX, 1f);
        }
    }

}
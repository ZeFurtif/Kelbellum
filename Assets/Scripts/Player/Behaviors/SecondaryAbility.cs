using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class SecondaryAbility : MonoBehaviour
{
    private PlayerData playerData;
    private SecondaryData secondaryData;
    private PlayerHealth playerHealth;

    public bool canUseSecondary;

    public GameObject[] targets;

    private float lastUseTime = -9999;
    private GameObject[] enemies;
    private GameObject[] enemiesHit;
    private GameObject closestEnemy;

    void Start()
    {
        playerData = gameObject.GetComponent<PlayerData>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        secondaryData = playerData.currentSecondaryData;
        canUseSecondary = secondaryData;
    }

    public void LoadPlayerData(PlayerData newData)
    {
        playerData = newData;
    }

    public void LoadSecondaryData(SecondaryData newSecondaryData)
    {
        secondaryData = newSecondaryData;
    }

    public void OnSecondary(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canUseSecondary && CooldownCheck())
        {
            UpdateEnemyList();
            switch (secondaryData.type)
            {
                case SecondaryData.SecondaryType.melee:
                    MeleeEnemyCheck();
                    Hit();
                    break;
                case SecondaryData.SecondaryType.shield:
                    UseShield();
                    break;
                
                case SecondaryData.SecondaryType.heal:
                    UseHeal();
                    break;

                case SecondaryData.SecondaryType.statsUp:
                    UseStatsUp();
                    break;

                case SecondaryData.SecondaryType.areaEffect:
                    break;
            }
            //UseFX();
            lastUseTime = Time.time;
        }
    }

    private bool CooldownCheck()
    {
        if (Time.time - lastUseTime < secondaryData.cooldown * playerData.cooldownAcceleration)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private Vector3 CalculateDamage()
    {
        return new Vector3(secondaryData.forceDamage * playerData.strength, secondaryData.precisionDamage * playerData.dexterity, secondaryData.magicDamage * playerData.arcana)* DoesItCrit();
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

    private void UseFX()
    {
        Debug.Log("remember me eliott");
    }

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


            if (dist < secondaryData.range && dotProduct > 0.5f)
            {
                enemiesInRange.Add(g);
            }
        }
        enemiesHit = enemiesInRange.ToArray();
    }

    private void Hit()
    {
        if (enemiesHit != null)
        {
            foreach (GameObject g in enemiesHit)
            {
                g.SendMessage("ApplyDamage", CalculateDamage());

                Vector3 knockbackVector = Vector3.Normalize(g.transform.position - this.gameObject.transform.position);
                g.SendMessage("GetKnockback", secondaryData.knockback * knockbackVector);
            }
        }
    }

    private void UseShield()
    {
        playerHealth.shieldValue += secondaryData.shieldValue;
        StartCoroutine(RemoveShield());
    }

    public IEnumerator RemoveShield()
    {
        while (playerHealth.shieldValue > 0)
        {
            playerHealth.shieldValue -= secondaryData.shieldValue / 10;
            yield return new WaitForSeconds(secondaryData.shieldDuration / 10);
        }
        playerHealth.shieldValue = 0;
    }

    private void UseHeal()
    {
        playerHealth.Heal(secondaryData.healValue);
    }

    private void UseStatsUp()
    {
        playerData.AddStats(secondaryData.statsUpData);
        StartCoroutine(RemoveStats());
    }

    IEnumerator RemoveStats()
    {
        yield return new WaitForSeconds(secondaryData.statsUpDuration);
        playerData.SubstractStats(secondaryData.statsUpData);
    }


    private void UseAOE()
    {
        Instantiate(secondaryData.areaObject, transform, true);
    }

}

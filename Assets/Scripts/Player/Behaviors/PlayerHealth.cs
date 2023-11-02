using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerData playerData;

    private float health = 50f;
    private float maxHealth = 50f;

    public float shieldValue = 0f;
    
    private void Start() 
    {
        playerData = gameObject.GetComponent<PlayerData>();
        GetData();
    }

    void GetData()
    {
        maxHealth = playerData.health;
        health = maxHealth;
    }

    public void LoadPlayerData(PlayerData newData)
    {
        playerData = newData;
        GetData();
    }

    public void ApplyDamage(float damage)
    {
        if (shieldValue > 0)
        {
            Debug.Log("TANKED");
            float da = damage;
            damage -= shieldValue;
            shieldValue -= da;
        }
        if (damage > 0)
        {
            health -= damage;
        }
        
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerData playerData;

    private float maxHealth = 50f;
    private Slider healthDisplay;

    [HideInInspector]
    public float shieldValue = 0f;
    
    private void Awake()
    {
        playerData = gameObject.GetComponent<PlayerData>();

        GetData();
    }

    private void Start() 
    {
        GetData();
        
        UpdateDisplay();
    }

    private void Update()
    {
        if (playerData.health < maxHealth && playerData.regeneration != 0)
        {
            playerData.health += playerData.regeneration * Time.deltaTime;
            if (playerData.health > maxHealth)
            {
                playerData.health = maxHealth;
            }
        }        
    }

    void GetData()
    {
        maxHealth = playerData.currentCharacter.health;
    }

    public void LoadPlayerData(PlayerData newData)
    {
        playerData = newData;
        GetData();
    }

    public void GetSlider(Slider display)
    {
        healthDisplay = display;
        UpdateDisplay();
    }

    public void ApplyDamage(Damage damage)
    {
        float cumulativeDamage = DamageAfterResistances(damage);
        if (shieldValue > 0)
        {
            //Debug.Log("TANKED");
            float da = cumulativeDamage;
            cumulativeDamage -= shieldValue;
            shieldValue -= da;
        }
        if (cumulativeDamage > 0)
        {
            playerData.health -= cumulativeDamage;
        }
        
        if (playerData.health <= 0)
        {
            this.gameObject.SetActive(false);
        }

        UpdateDisplay();
    }

    public void Heal(float healAmount)
    {
        //Debug.Log("HEAL");
        playerData.health += healAmount;
        if (playerData.health > maxHealth)
        {
            playerData.health = maxHealth;   
        }
        UpdateDisplay();
    }
    
    public float DamageAfterResistances(Damage damage)
    {
        return (damage.damageVector.x / playerData.strResistance + damage.damageVector.y / playerData.dexResistance + damage.damageVector.z / playerData.arcResistance);
    }

    public void UpdateDisplay()
    {
        if (healthDisplay != null)
        {
            healthDisplay.value = playerData.health;   
            healthDisplay.maxValue = maxHealth;
        }
    }
}

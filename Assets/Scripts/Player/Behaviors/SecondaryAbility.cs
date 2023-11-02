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

    private float lastUseTime = 0;

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
            switch (secondaryData.type)
            {
                case SecondaryData.SecondaryType.shield:
                    UseShield();
                    break;
                
                case SecondaryData.SecondaryType.heal:
                    break;

                case SecondaryData.SecondaryType.statsUp:
                    break;
            }
            lastUseTime = Time.time;
        }
    }

    private bool CooldownCheck()
    {
        if (Time.time - lastUseTime < secondaryData.cooldown)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void UseShield()
    {
        playerHealth.shieldValue += secondaryData.value;
        StartCoroutine(RemoveShield());
    }

    IEnumerator RemoveShield()
    {
        while (playerHealth.shieldValue > 0)
        {
            playerHealth.shieldValue -= secondaryData.value / 10;
            yield return new WaitForSeconds(.1f);
        }
        playerHealth.shieldValue = 0;
    }

}

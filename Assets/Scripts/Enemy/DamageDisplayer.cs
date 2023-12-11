using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageDisplayer : MonoBehaviour
{

    public float lifetime = 0.4f;

    public Color strColor = Color.red;
    public Color dexColor = Color.green;
    public Color arcColor = Color.blue; 
    
    public TextMeshProUGUI textComponent;


    public Color ColorSwitch(Vector3 damage)
    {
        if (damage.x >= damage.y && damage.x >= damage.z) // STRENGHT
        {
            return strColor;
        }
        else if (damage.y >= damage.x && damage.y >= damage.z) // DEX
        {
            return dexColor;
        } 
        else // ARC
        {
            return arcColor;
        }
    }

    public float SumDamage(Vector3 damage)
    {
        return damage.x + damage.y + damage.z;
    }

    public void ModifyDisplay(Vector3 damage)
    {
        textComponent = gameObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = "" + Math.Round(SumDamage(damage), 1);
        textComponent.color = ColorSwitch(damage);
        
        Destroy(transform.parent.gameObject, lifetime);
        StartCoroutine(FadeAndMove());
    }

    IEnumerator FadeAndMove()
    {
        while (textComponent.alpha > 0)
        {
            textComponent.alpha -= 0.1f;
            transform.position += new Vector3(0,0.4f,0);
            yield return new WaitForSeconds(.1f);
        }
    }

}

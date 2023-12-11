using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Character Info")]
    public TextMeshProUGUI characterName;
    public Image characterIcon;

    [Header("Gradient")]
    public Gradient gradient;
    public Slider slider;
    public Image fill;

    public void GetGradient(Gradient newGradient)
    {
        gradient = newGradient;
    }

    public void UpdateColor()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void UpdateCharacterDisplay(int characterIndex)
    {
        characterName.text = DataManager.Instance.allCharactersData.charactersData[characterIndex].characterName;
        characterIcon.sprite = DataManager.Instance.allCharactersData.charactersData[characterIndex].characterSprite;
    }
}

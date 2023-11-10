using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterSelectorManager : MonoBehaviour
{
    [Header("Selection")]
    public int characterIndex = 0;
    public bool isReady = false;

    [Header("Layout")]
    public GameObject[] autoLayout;
    [Space(10)]
    public TextMeshProUGUI playerIndex;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterDesc;
    public Image characterIcon;
    public Slider strenghtSlider;
    public Slider dexteritySlider;
    public Slider arcanaSlider;

    public GameObject readyOverlay;

    [Header("Input & Data")]
    private PlayerInput playerInput;


    void Start()
    {
        playerInput = this.GetComponent<PlayerInput>();

        autoLayout = GameObject.FindGameObjectsWithTag("CS_AutoLayout");
        transform.SetParent(autoLayout[0].transform);

        autoLayout[0].SendMessage("addPlayer");

        transform.localScale = new Vector3(1,1,1);

        this.gameObject.SendMessage("PushTo", playerInput.playerIndex);

        SavePIData();
        UpdateUI();
    }

    void CheckIndexRange()
    {
        characterIndex = (characterIndex+ DataManager.Instance.allCharactersData.charactersData.Count) % DataManager.Instance.allCharactersData.charactersData.Count;
        if (characterIndex < 0)
        {
            characterIndex *= -1;
        }
    }

    bool CheckUnlocked()
    {
        return DataManager.Instance.data.characters[characterIndex].unlocked;
    }

    public void GoPrevious()
    {
        characterIndex -= 1;
        CheckIndexRange();

        if(!CheckUnlocked())
        {
            GoPrevious();
        }

        UpdateUI();        
    }

    public void GoNext()
    {
        characterIndex += 1;
        CheckIndexRange();

        if(!CheckUnlocked())
        {
            GoNext();
        }

        UpdateUI();        
    }

    void UpdateUI()
    {
        playerIndex.text = "Player " + (playerInput.playerIndex + 1);
        characterName.text = DataManager.Instance.allCharactersData.charactersData[characterIndex].characterName;
        characterDesc.text = DataManager.Instance.allCharactersData.charactersData[characterIndex].oneLiner;

        characterIcon.sprite = DataManager.Instance.allCharactersData.charactersData[characterIndex].characterSprite;
        
        strenghtSlider.value = DataManager.Instance.allCharactersData.charactersData[characterIndex].characterSO.strength;
        dexteritySlider.value = DataManager.Instance.allCharactersData.charactersData[characterIndex].characterSO.dexterity;
        arcanaSlider.value = DataManager.Instance.allCharactersData.charactersData[characterIndex].characterSO.arcana;
    }

    void SavePIData()
    {
        DataManager.Instance.data.NewPLayer(playerInput.playerIndex, playerInput.currentControlScheme, playerInput.devices[0]);
    }

    public void GetReady()
    {
        isReady = !isReady;

        // UI UPDATE
        readyOverlay.SetActive(isReady);
        
        // VOTE TO GO TO THE GAME
        autoLayout[0].SendMessage("addReady", isReady);

        if(isReady)
        {
            UpdatePlayerInfo();
        }
    }

    public void UpdatePlayerInfo()
    {
        DataManager.Instance.data.players[playerInput.playerIndex].characterIndex = characterIndex;
    }
}



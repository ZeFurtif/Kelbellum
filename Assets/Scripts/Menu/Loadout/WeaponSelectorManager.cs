using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponSelectorManager : MonoBehaviour
{
    [Header("Selection")]
    public int characterIndex;
    public int primaryIndex = 0;
    public int secondaryIndex = 0;
    public bool isReady     = false;

    [Header("Layout")]
    public GameObject[] autoLayout;
    [Space(10)]
    public TextMeshProUGUI playerIndex;
    public TextMeshProUGUI primaryName;

    public GameObject readyOverlay;

    [Header("Input & Data")]
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = this.GetComponent<PlayerInput>();

        autoLayout = GameObject.FindGameObjectsWithTag("CS_AutoLayout");
        transform.SetParent(autoLayout[0].transform);

        autoLayout[0].SendMessage("addPlayer");

        transform.localScale = new Vector3(1,1,1);

        this.gameObject.SendMessage("PushTo", playerInput.playerIndex);

        characterIndex = DataManager.Instance.data.players[playerInput.playerIndex].characterIndex;

        UpdateUI();
    }

    void CheckIndexRange()
    {
        primaryIndex = (primaryIndex + DataManager.Instance.allCharactersData.charactersData[characterIndex].characterSO.primaries.Length) % DataManager.Instance.allCharactersData.charactersData[characterIndex].characterSO.primaries.Length;
        if (primaryIndex < 0)
        {
            primaryIndex *= -1;
        }
    }

    public void GoPrevious()
    {
        primaryIndex -= 1;
        CheckIndexRange();

        UpdateUI();        
    }

    public void GoNext()
    {
        primaryIndex += 1;
        CheckIndexRange();

        UpdateUI();        
    }

    void UpdateUI()
    {
        playerIndex.text = "Player " + (playerInput.playerIndex + 1);
        primaryName.text = DataManager.Instance.allCharactersData.charactersData[characterIndex].characterSO.primaries[primaryIndex].name;
    }

    void SavePrimaryIndex()
    {
        DataManager.Instance.data.players[System.Math.Abs(playerInput.playerIndex)].primaryIndex = primaryIndex;
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
            SavePrimaryIndex();
        }
    }
}

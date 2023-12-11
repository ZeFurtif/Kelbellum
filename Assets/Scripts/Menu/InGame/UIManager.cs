using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Settings")]
    public bool doDisplay = true;
    private GameObject[] players;

    [Header("Health Display Settings")]
    public bool healthDisplay;
    public Gradient healthGradient;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        DealGradients();
        DealSliders();
        if (DataManager.Instance)
        {
            //Debug.Log("Data Instance exists");
            UpdateCharacterDisplay();
        }
    }

    void DealGradients()
    {
        int pIndex = 0;
        foreach (GameObject p in players)
        {
            transform.GetChild(pIndex).SendMessage("GetGradient", healthGradient);
            pIndex += 1;
        } 
    }

    void DealSliders()
    {
        int pIndex = 0;
        foreach (GameObject p in players)
        {
            Slider pSlider = transform.GetChild(pIndex).Find("Slider").GetComponent<Slider>();
            p.SendMessage("GetSlider", pSlider);
            pIndex += 1;
        } 

        for (int i = pIndex ; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void UpdateCharacterDisplay()
    {
        int pIndex = 0;
        foreach (GameObject p in players)
        {
            transform.GetChild(pIndex).SendMessage("UpdateCharacterDisplay", DataManager.Instance.data.players[pIndex].characterIndex);
            pIndex += 1;
        } 
    }
}

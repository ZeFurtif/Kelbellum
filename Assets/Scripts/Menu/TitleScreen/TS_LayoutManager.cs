using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_LayoutManager : MonoBehaviour
{

    [Header("Menus")]

    public GameObject titleMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    [Space(10)]

    public GameObject saveSelectionMenu;
    public GameObject characterSelctionMenu;


    void Start()
    {
        ToTitleMenu();
    }

    public void ToTitleMenu()
    {
        titleMenu.SetActive(true);

        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        saveSelectionMenu.SetActive(false);
        characterSelctionMenu.SetActive(false);
    }

    public void ToCreditsMenu()
    {
        creditsMenu.SetActive(true);

        optionsMenu.SetActive(false);
        titleMenu.SetActive(false);
        saveSelectionMenu.SetActive(false);
        characterSelctionMenu.SetActive(false);
    }

    public void ToOptionsMenu()
    {
        optionsMenu.SetActive(true);

        titleMenu.SetActive(false);
        creditsMenu.SetActive(false);
        saveSelectionMenu.SetActive(false);
        characterSelctionMenu.SetActive(false);
    }

    public void ToSaveSelection()
    {
        saveSelectionMenu.SetActive(true);

        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        titleMenu.SetActive(false);
        characterSelctionMenu.SetActive(false);
    }

    public void ToCharacterSelection()
    {
        characterSelctionMenu.SetActive(true);

        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        saveSelectionMenu.SetActive(false);
        titleMenu.SetActive(false);
    }

}

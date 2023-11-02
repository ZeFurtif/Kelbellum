using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{

    [Header("Saves")]
    private string saveDir = @"\saves";

    public int nbOfSaves = -1;
    public int maxNbOfSaves = 3;
    public string[] savesPath;

    [Header("Save UI Layout")]
    public GameObject saveAutoLayout;
    public GameObject newSaveButton;
    public GameObject saveButtonTemplate;

    [Header("Data")]
    public DataManager datam;
    public SaveData tempData;

    [Header("Layout Manager")]
    public TS_LayoutManager layoutManager;


    void OnEnable()
    {
        if(!Directory.Exists(Application.persistentDataPath + saveDir))
        {
            Directory.CreateDirectory(Application.persistentDataPath + saveDir);
        }

        GetSaves();
        SetupSaveLayout();
    }

    void GetSaves()
    {
        savesPath = Directory.GetFiles(Application.persistentDataPath + saveDir, "*", SearchOption.TopDirectoryOnly);
        nbOfSaves = savesPath.Length;
    }

    public void CreateSave()
    {
        datam.CreateJson(datam.data, nbOfSaves);
        GoToNext(nbOfSaves);
    }

    public void ReadSave(int saveIndex)
    {
        datam.LoadJson(datam.data, saveIndex);
    }

    public void GoToNext(int saveIndex)
    {
        ReadSave(saveIndex);
        layoutManager.ToCharacterSelection();
    }

    private void SetupSaveLayout()
    {
        if (nbOfSaves < maxNbOfSaves) 
        {
            newSaveButton.SetActive(true);
        }

        if (nbOfSaves > 0)
        {
            int currentSave = 0;
            foreach(string savePath in savesPath)
            {
                GameObject saveIndexText = saveButtonTemplate.transform.Find("Save Index").gameObject;
                saveIndexText.GetComponent<TextMeshProUGUI>().text = "Save " + currentSave;
                
                GameObject saveDateText = saveButtonTemplate.transform.Find("Save Date").gameObject;
                datam.LoadJson(tempData, currentSave);
                saveDateText.GetComponent<TextMeshProUGUI>().text = "" + tempData.saveDate; 

                GameObject saveButton = Instantiate(saveButtonTemplate, saveButtonTemplate.transform.position, Quaternion.identity);
                saveButton.transform.SetParent(saveAutoLayout.transform, true);
                saveButton.name = "" + currentSave;
                saveButton.SetActive(true);

                Button btn = saveButton.GetComponent<Button>();
                btn.onClick.AddListener(delegate {GoToNext(int.Parse(saveButton.name));});

                currentSave = currentSave + 1;
            }
        }
    }
}

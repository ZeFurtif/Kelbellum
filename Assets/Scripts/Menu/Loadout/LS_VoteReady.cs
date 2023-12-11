using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LS_VoteReady : MonoBehaviour
{
    public GameObject nextSelectionMenu;

    public int playersSpawned = 0;
    public int playersReady = 0;
    

    void CheckIfElected()
    {
        if (playersSpawned != 0 && playersSpawned == playersReady)
        {
            Next();
        }
    }

    public void addPlayer()
    {
        playersSpawned += 1;
    }

    public void addReady(bool isReady)
    {
        if (isReady)
        {
            playersReady += 1;
        }
        else
        {
            playersReady -= 1;
        }
        CheckIfElected();
    }

    public void Next()
    {
        if(nextSelectionMenu != null)
        {
            nextSelectionMenu.SetActive(true);
            this.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            ToLevel();
        }
    }

    void ToLevel()
    {        
        SceneManager.LoadScene(DataManager.Instance.data.currentArea, LoadSceneMode.Single);
    }
}

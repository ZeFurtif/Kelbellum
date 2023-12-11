using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TS_VoteReady : MonoBehaviour
{
    public int playersSpawned = 0;
    public int playersReady = 0;
    

    void CheckIfElected()
    {
        if (playersSpawned != 0 && playersSpawned == playersReady)
        {
            ToGame();
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

    public void ToGame()
    {
        DataManager.Instance.SaveJson(DataManager.Instance.data, DataManager.Instance.data.saveIndex);

        DataManager.Instance.data.SaveLevelData(DataManager.Instance.data.currentArea, 0);
        
        SceneManager.LoadScene(DataManager.Instance.data.currentArea, LoadSceneMode.Single);

    }
}

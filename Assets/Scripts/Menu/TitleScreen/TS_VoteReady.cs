using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_VoteReady : MonoBehaviour
{
    public int playersSpawned = 0;
    public int playersReady = 0;
    

    void CheckIfElected()
    {
        if (playersSpawned != 0 && playersSpawned == playersReady)
        {
            Debug.Log("GO!");
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
}

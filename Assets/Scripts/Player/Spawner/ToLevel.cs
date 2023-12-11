using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel : MonoBehaviour
{
    [Header("Teleporting To")]
    public string sceneName;
    public int spawnerIndex;

    public bool isCombatLevel;

    [Header("Votes")]
    private int playerCount;
    public int playerVotes = 0;

    void Start()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        //Debug.Log(playerCount);

        playerVotes = 0;
    }

    void UpdatePlayerCount()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        //Debug.Log(playerCount);
    }

    void AddVote()
    {
        playerVotes += 1;
    }

    void ToScene()
    {
        DataManager.Instance.data.spawnIndex = spawnerIndex;
        DataManager.Instance.data.currentArea = sceneName;

        if (isCombatLevel)
        {
            SceneManager.LoadScene("0_LoadoutSelection", LoadSceneMode.Single);
        }
        else
        {
            //DataManager.Instance.SaveJson(DataManager.Instance.data, DataManager.Instance.data.saveNumber);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLIDED");
        UpdatePlayerCount();
        if (collision.gameObject.tag == "PlayerObjects")
        {
            AddVote();
            if (playerCount-playerVotes <= 1)
            {
                ToScene();
            }
        }
    }
}

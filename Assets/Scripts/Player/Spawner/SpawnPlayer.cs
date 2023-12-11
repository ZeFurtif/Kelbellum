using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject playerPrefab;

    public bool isCombatLevel = false; 

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("Spawning");
        if (DataManager.Instance)
        {
            SpawnPlayers();
        }
    }

    public void SpawnPlayers()
    {
        var players = DataManager.Instance.data.players;
        //Debug.Log(players[0]);
        foreach(var player in players)
        {

            var spawned = PlayerInput.Instantiate(playerPrefab, player.index, player.controlScheme, -1, player.device);

            spawned.GetComponent<PlayerData>().LoadPlayerData(DataManager.Instance.allCharactersData.charactersData[player.characterIndex].characterSO);

            if ( isCombatLevel ) // GIVES THE ABILITIES
            {
                spawned.GetComponent<PlayerData>().LoadPrimaryData(DataManager.Instance.allCharactersData.charactersData[player.characterIndex].characterSO.primaries[player.primaryIndex]);
            }

            spawned.transform.position = transform.GetChild(DataManager.Instance.data.spawnIndex).position + new Vector3(player.index * 2 , 0, player.index * 2);

        }
    }
}

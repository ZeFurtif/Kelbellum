using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LS_SpawnPlayers : MonoBehaviour
{

    public GameObject playerPrefab;

    void OnEnable()
    {
        Spawn();     
    }

    void Spawn()
    {
        var players = DataManager.Instance.data.players;
        foreach(var player in players)
        {
            var spawned = PlayerInput.Instantiate(playerPrefab, player.index, player.controlScheme, -1, player.device);
        }
    }
}

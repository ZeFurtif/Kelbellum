using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PakMan : MonoBehaviour
{

    public bool loop;

    public Vector2 areaSize;
    public float floorHeight;

    private GameObject[] players;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        if (loop)
        {
            foreach (GameObject p in players)
            {
                MovePlayer(p);
            }
        }
    }

    void MovePlayer(GameObject p)
    {
        int current_floor = (int)(p.transform.position.y / floorHeight);

        float player_x = p.transform.position.x;
        float player_z = p.transform.position.z + (current_floor*floorHeight);

        float check_x = (( player_x + areaSize.x/2 + areaSize.x ) % areaSize.x) - areaSize.x/2;
        float check_z = (( player_z + areaSize.y/2 + areaSize.y ) % areaSize.y) - areaSize.y/2;

        if(!(check_x == player_x && check_z == player_z))
        {
            //Debug.Log("switch : " + check_z + " " + player_z + " " + check_x + " " + player_x + " " + current_floor);
            
            Vector3 new_pos = new Vector3(check_x, 0f, check_z);
        
            while(Physics.CheckSphere(new Vector3( check_x, (current_floor+1) * floorHeight, check_z - (current_floor+1) * floorHeight), 0.3f))
            {
                new_pos.y += floorHeight;
                new_pos.z -= floorHeight;
                current_floor += 1;
            }

            if(Physics.CheckSphere(new Vector3(new_pos.x, new_pos.y + 1f, new_pos.z), 0.3f))
            {
                new_pos.y += floorHeight;
                new_pos.z -= floorHeight;

                if(!Physics.CheckSphere(new_pos, 0.4f))
                {
                    new_pos.z += floorHeight;
                }
            }

            // ACTUAL MOVE
            CharacterController cc = p.GetComponent<CharacterController>();
            cc.enabled = false;
            p.gameObject.transform.position = new_pos;
            cc.enabled = true;
        }
    }
}

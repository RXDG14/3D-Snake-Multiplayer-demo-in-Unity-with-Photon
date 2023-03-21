using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    private void Start()
    {
        SpawnPlayers();    
    }

    void SpawnPlayers()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.Instantiate(player1.name, new Vector3(-5, 1, -8), Quaternion.identity);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.Instantiate(player2.name, new Vector3(5, 1, -8), Quaternion.identity);
        }
    }
}

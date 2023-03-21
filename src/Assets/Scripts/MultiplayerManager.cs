using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text plsWait;
    [SerializeField] GameObject foodPrefab;

    List<PhotonView> SnakePlayerList;// = new List<PhotonView>();

    int food = 1;
    int scoreValue = 0;
    int countdownTime = 3;
    bool twoPlayersPresent = false;
    public PhotonView view;

    [SerializeField] SpawnPlayer spawnManager;

    private void Start()
    {
        InvokeRepeating(nameof(CheckNumberOfPlayers), 1f, 1f);
    }

    public void CheckNumberOfPlayers()
    {
        if (twoPlayersPresent == false)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                twoPlayersPresent = true;
                StartCoroutine(CountDown());
            }
            else
            {
                plsWait.text = "Please wait for players to join";
            }
        }
        else
        {
            return;
        }
    }

    public void UpdateScore()
    {
        if (view.IsMine)
        {
            scoreValue++;
            scoreText.text = "Score: " + scoreValue.ToString();
        }
    }

    IEnumerator CountDown()
    {
        while (countdownTime > 0)
        {
            plsWait.text = "Starting in " + countdownTime.ToString() + " seconds";
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        if (countdownTime == 0)
        {
            plsWait.gameObject.SetActive(false);
            view.RPC("StartGame", RpcTarget.All);
        }
    }

    [PunRPC]
    public void StartGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Snake");
        foreach (GameObject player in players)
        {
            SnakeMultiplayer playerController = player.GetComponent<SnakeMultiplayer>();
            if (playerController != null)
            {
                playerController.canMove = true;
            }
        }
    }

    [PunRPC]
    public void StopGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Snake");
        foreach (GameObject player in players)
        {
            SnakeMultiplayer playerController = player.GetComponent<SnakeMultiplayer>();
            if (playerController != null)
            {
                playerController.canMove = false;
            }
        }
    }
}

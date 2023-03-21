using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField createRoomNameInput;
    [SerializeField] TMP_InputField joinRoomNameInput;
    [SerializeField] Button createRoomButton;
    [SerializeField] Button joinRoomButton;

    int i = 1;

    public void OnCreateRoomClicked()
    {
        i++;
        createRoomNameInput.interactable = false;
        createRoomButton.interactable = false;
        joinRoomNameInput.interactable = false;
        joinRoomButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnJoinRoomClicked()
    {
        i--;
        joinRoomNameInput.interactable = false;
        joinRoomButton.interactable = false;
        createRoomNameInput.interactable = false;
        createRoomButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if(i == 2)
        {
            PhotonNetwork.CreateRoom(createRoomNameInput.text);
        }
        if(i == 0)
        {
            PhotonNetwork.JoinRoom(joinRoomNameInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }
}

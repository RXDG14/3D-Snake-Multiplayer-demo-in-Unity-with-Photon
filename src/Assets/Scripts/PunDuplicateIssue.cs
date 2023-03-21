using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunDuplicateIssue : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
    }
}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMultiplayer : MonoBehaviourPunCallbacks
{
    [SerializeField] PhotonView view;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            gameObject.SetActive(false);
            Invoke(nameof(SetNewFoodPos), 2f);
        }
    }

    public void SetNewFoodPos()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-10, 10), 1, Random.Range(-8, 8));
        transform.position = spawnPos;
        gameObject.SetActive(true);
    }
}

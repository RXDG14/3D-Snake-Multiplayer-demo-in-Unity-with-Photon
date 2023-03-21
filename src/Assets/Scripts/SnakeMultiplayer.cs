using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeMultiplayer : MonoBehaviourPunCallbacks
{
    List<GameObject> Tail = new List<GameObject>();
    List<Vector3> PositionHistory = new List<Vector3>();

    [SerializeField] PhotonView view;
    [SerializeField] GameObject tailPrefab;
    [SerializeField] TMP_Text scoreText;

    [SerializeField] bool canDie = false;
    [SerializeField] int gap = 5;
    [SerializeField] int scoreValue = 0;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 180f;

    int i = 1;
    public bool canMove = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (view.IsMine && canMove)
        {
            Movement();
            SnakeRotation();
        }
    }

    private void Movement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        PositionHistory.Insert(0, transform.position);

        for (int i = 0; i < Tail.Count; i++)
        {
            Vector3 point = PositionHistory[Mathf.Clamp(i * gap, 0, PositionHistory.Count - 1)];
            Vector3 moveDirection = point - Tail[i].transform.position;
            Tail[i].transform.position += moveDirection * moveSpeed * Time.deltaTime;
            Tail[i].transform.LookAt(point);
        }
    }

    private void SnakeRotation()
    {
        float dir = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * dir * rotationSpeed * Time.deltaTime);
    }

    private void GrowSnake()
    {
        if (view.IsMine)
        {
            Vector3 pos = new Vector3(transform.position.x, -2, transform.position.z);
            GameObject tail = PhotonNetwork.Instantiate(tailPrefab.name, pos, Quaternion.identity);
            Tail.Add(tail);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MovingFood"))
        {
            GrowSnake();
            UpdateScore();
        }
        if (other.gameObject.CompareTag("Tail") && Tail.Count > 1 && other.gameObject != Tail[0])
        {
            PhotonNetwork.Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (view.IsMine)
            {
                gameObject.SetActive(false);
                foreach (GameObject t in Tail)
                {
                    //PhotonNetwork.Destroy(t);
                    Destroy(t);
                }
            }
        }
    }

    private void UpdateScore()
    {
        if (view.IsMine)
        {
            scoreValue++;
            scoreText.text = "Score: " + scoreValue.ToString();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    List<GameObject> Tail = new List<GameObject>();
    List<Vector3> PositionHistory = new List<Vector3>();

    [SerializeField] GameObject tailPrefab;

    [SerializeField] bool canDie = false;
    [SerializeField] int gap = 5;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 180f;

    int i = 1;

    private void Start()
    {

    }

    private void Update()
    {
        Movement();
        SnakeRotation();
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
        Vector3 pos = new Vector3(transform.position.x, -2, transform.position.z);
        GameObject tail = Instantiate(tailPrefab, pos, Quaternion.identity);
        Tail.Add(tail);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            i++;
            GrowSnake();
            other.gameObject.SetActive(false);
            GameManager.GM_Instance.SetNewFoodPos(i);
            GameManager.GM_Instance.UpdateScore();
        }
        if (other.gameObject.CompareTag("MovingFood"))
        {
            i++;
            GrowSnake();
            other.gameObject.SetActive(false);
            GameManager.GM_Instance.SetNewFoodPos(i);
            GameManager.GM_Instance.UpdateScore();
        }
        if (other.gameObject.CompareTag("Tail") && other.gameObject != Tail[0])
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}

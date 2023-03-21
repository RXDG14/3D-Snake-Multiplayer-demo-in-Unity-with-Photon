using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM_Instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject foodPrefab;
    [SerializeField] GameObject movingFoodPrefab;

    int scoreValue = 0;

    private void Awake()
    {
        GM_Instance = this;
    }

    private void Start()
    {
        movingFoodPrefab.SetActive(false);
    }

    public void SetNewFoodPos(int i)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-10, 10), 1, Random.Range(-8, 8));
        if (i%2 != 0)
        {
            if (Physics.CheckBox(spawnPos, new Vector3(2, 2, 2), Quaternion.identity, 0))
            {
                SetNewFoodPos(i);
            }
            else
            {
                foodPrefab.transform.position = spawnPos;
                foodPrefab.SetActive(true);
            }
        }
        else
        {
            if (Physics.CheckBox(spawnPos, new Vector3(2, 2, 2), Quaternion.identity, 0))
            {
                SetNewFoodPos(i);
            }
            else
            {
                movingFoodPrefab.transform.position = spawnPos;
                movingFoodPrefab.SetActive(true);
            }
        }
    }

    public void UpdateScore()
    {
        scoreValue++;
        scoreText.text = "Score: " + scoreValue.ToString();
    }
}

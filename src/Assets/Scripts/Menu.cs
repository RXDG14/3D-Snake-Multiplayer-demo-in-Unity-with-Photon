using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnSinglePlayerPressed()
    {
        SceneManager.LoadScene(2);
    }

    public void OnMultiPlayerPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }
}

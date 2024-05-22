using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1.0f;
    }

    public void Exit() => Application.Quit();

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Start Button Click");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("I'll be back");
        Application.Quit();
    }
}

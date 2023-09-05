using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void StartGame()
    {
        audioManager.playPrologueBGM();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Debug.Log("I'll be back");
        Application.Quit();
    }
}

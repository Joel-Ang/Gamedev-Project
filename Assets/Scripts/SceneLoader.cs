﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void MapScene()
    {
        SceneManager.LoadScene(2);
    }

    public void Stage1()
    {
        SceneManager.LoadScene(3);
    }
    public void TutorialStage()
    {
        SceneManager.LoadScene(4);
    }

    public void EndScene()
    {
        SceneManager.LoadScene(5);
    }

    public void CongratsScene()
    {
        SceneManager.LoadScene(6);
    }
}

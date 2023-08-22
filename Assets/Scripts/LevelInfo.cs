using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    public GameObject LevelInfo1;
    public GameObject LevelInfo2;
    public GameObject LevelInfo3;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    public void whenStage1ButtonClicked()
    {
        if (LevelInfo1.activeInHierarchy == false)
        {
            LevelInfo1.SetActive(true);
            LevelInfo2.SetActive(false);
            LevelInfo3.SetActive(false);
        }
        else
            LevelInfo1.SetActive(false);

     
    }

    public void whenStage2ButtonClicked()
    {
        if (LevelInfo2.activeInHierarchy == false)
        {
            LevelInfo1.SetActive(false);
            LevelInfo2.SetActive(true);
            LevelInfo3.SetActive(false);
        }
        else
            LevelInfo2.SetActive(false);
    }

    public void whenStage3ButtonClicked()
    {
        if (LevelInfo3.activeInHierarchy == false)
        {
            LevelInfo1.SetActive(false);
            LevelInfo2.SetActive(false);
            LevelInfo3.SetActive(true);
        }
        else
            LevelInfo3.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelInfoManager : MonoBehaviour
{
    public GameObject LevelInfo1;
    public GameObject LevelInfo2;
    public GameObject LevelInfo3;

    public TextMeshProUGUI Stage1Info;
    public TextMeshProUGUI Stage2Info;
    public TextMeshProUGUI Stage3Info;

    static int[] NoEnemies = { 3, 4, 5, 6, 7, 8 };
    static string[] EnemType = { "Bugs", "Boss" };
    static string[] Background = { "Castle", "Castle Gate", "Village", "Forest" };
    static string[] QuesIndex = { "[0-5]", "[6-10]", "[11-15]", "[16-20]", "[21-25]" };


    // Start is called before the first frame update
    void Start()
    {
        string Stage1Text = "def Stage1(): \n \n  noEnem: " + NoEnemies[0] + "\n \n EnemType = " + EnemType[0] + "\n \n StageInfo = { \n \n Background: " + Background[1] + ", \n \n QnIndex: " + QuesIndex[0] + " \n \n };";
        Stage1Info.text = Stage1Text;

        string Stage2Text = "def Stage2(): \n \n  noEnem: " + NoEnemies[0] + "\n \n EnemType = " + EnemType[0] + "\n \n StageInfo = { \n \n Background: " + Background[2] + ", \n \n QnIndex: " + QuesIndex[1] + " \n \n };";
        Stage2Info.text = Stage2Text;

        string Stage3Text = "def Stage3(): \n \n  noEnem: " + NoEnemies[2] + "\n \n EnemType = " + EnemType[1] + "\n \n StageInfo = { \n \n Background: " + Background[3] + ", \n \n QnIndex: " + QuesIndex[2] + " \n \n };";
        Stage3Info.text = Stage3Text;
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

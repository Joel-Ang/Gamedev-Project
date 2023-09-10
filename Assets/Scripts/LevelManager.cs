using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject stageMenu;
    public TMP_Text stageInfo;

    public Button stage0Button;
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;

    public static int stage;
    public static int enemyCount; // = { 3, 4, 5, 6, 7, 8 };
    public static string[] enemyType; // = { "Bugs", "Boss" };
    public static string battleBg; // = { "Castle", "Castle Gate", "Village", "Forest" };
    public static int quesIndex; // = { "[0-5]", "[6-10]", "[11-15]", "[16-20]", "[21-25]" };

    public static bool tutorialComplete = false;
    public static bool stage1Complete = false;
    public static bool stage2Complete = false;
    public static bool stage3Complete = false;

    public static float stage1Accuracy = 0;
    public static string stage1TotalTime = "null";
    public static float stage2Accuracy = 0;
    public static string stage2TotalTime = "null";
    public static float stage3Accuracy = 0;
    public static string stage3TotalTime = "null";

    string stageText;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialComplete)
        {
            stage0Button.transform.Find("ActiveState").gameObject.SetActive(false);
            stage0Button.transform.Find("DoneState").gameObject.SetActive(true);
            stage1Button.interactable = true;

            stage1Button.transform.Find("ActiveState").gameObject.SetActive(true);
            stage1Button.interactable = true;
        }
        if (stage1Complete)
        {
            stage1Button.transform.Find("ActiveState").gameObject.SetActive(false);
            stage1Button.transform.Find("DoneState").gameObject.SetActive(true);
            stage1Button.interactable = true;

            stage2Button.transform.Find("ActiveState").gameObject.SetActive(true);
            stage2Button.interactable = true;
        }
        if (stage2Complete)
        {
            stage2Button.transform.Find("ActiveState").gameObject.SetActive(false);
            stage2Button.transform.Find("DoneState").gameObject.SetActive(true);
            stage2Button.interactable = true;

            stage3Button.transform.Find("ActiveState").gameObject.SetActive(true);
            stage3Button.interactable = true;
        }
        if (stage3Complete)
        {
            stage3Button.transform.Find("ActiveState").gameObject.SetActive(true);
            stage3Button.transform.Find("DoneState").gameObject.SetActive(true);
            stage3Button.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLevelInfo(int stageNumber, string levelBg, string[] type, int qIndex, string topic, float accuracy, string totaltime)
    {
        //get level info
        stage = stageNumber;
        battleBg = levelBg;
        enemyCount = type.Length;
        enemyType = type;
        quesIndex = qIndex;

        string accuracyDisplay = "";
        if (accuracy == 0)
        {
            accuracyDisplay = "null";
        }
        else
        {
            accuracyDisplay = accuracy.ToString("F1") + "%";
        }

        //set level info
        if (stageNumber == 0) //tutorial stage
        {
            stageText = "<color=#9768d1>def</color> <color=#458edd><b>" + battleBg + "</b></color>():" +
                            "\n     StageInfo = {" +
                            "\n          <color=#6f7580>EnemyNumber:</color>  <color=#ca9759>" + enemyCount + "</color>" +
                            "\n          <color=#6f7580>EnemyType:</color>  <color=#8cba76>" + enemyType[enemyType.Length - 1] + "</color>" +
                            "\n          <color=#6f7580>Topic:</color>  <color=#8cba76>" + topic + "</color>" + " }";
        }
        else
        {
            stageText = "<color=#9768d1>def</color> <color=#458edd><b>" + battleBg + "</b></color>():" +
                            "\n     StageInfo = {" +
                            "\n          <color=#6f7580>EnemyNumber:</color>  <color=#ca9759>" + enemyCount + "</color>" +
                            "\n          <color=#6f7580>EnemyType:</color>  <color=#8cba76>" + enemyType[enemyType.Length - 1] + "</color>" +
                            "\n          <color=#6f7580>Topic:</color>  <color=#8cba76>" + topic + "</color>" + " }" +
                            "\n     BestScore = {" +
                            "\n          <color=#6f7580>Accuracy:</color>  <color=#ca9759>" + accuracyDisplay + "</color>" +
                            "\n          <color=#6f7580>TotalTimeTaken:</color>  <color=#ca9759>" + totaltime + "</color>" + " }";
        }
    }

    public void PlayStage()
    {
        if (stage == 0)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    public void CloseStageMenu()
    {
        stageMenu.SetActive(false);
    }


    public void Stage0()
    {
        string[] enemType = { "Weak", "Weak" };
        SetLevelInfo(0, "Castle", enemType, 0, "Python Basics", 0, "-");

        stageInfo.text = stageText;
        stageMenu.SetActive(true);
    }

    public void Stage1()
    {
        string[] enemType = { "Weak", "Weak", "Weak" };
        SetLevelInfo(1, "CastleGates", enemType, 8, "Python Data Types & Operations", stage1Accuracy, stage1TotalTime);

        stageInfo.text = stageText;
        stageMenu.SetActive(true);
    }

    public void Stage2()
    {
        string[] enemType = { "Weak", "Weak", "Strong" };
        SetLevelInfo(2, "Village", enemType, 15, "Python Modules", stage2Accuracy, stage2TotalTime);

        stageInfo.text = stageText;
        stageMenu.SetActive(true);
    }

    public void Stage3()
    {
        string[] enemType = { "Strong", "Strong", "Boss" };
        SetLevelInfo(3, "Forest", enemType, 24, "Python Functions", stage3Accuracy, stage3TotalTime);

        stageInfo.text = stageText;
        stageMenu.SetActive(true);
    }
}

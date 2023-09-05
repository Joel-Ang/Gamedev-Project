using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject stage1Menu;
    public GameObject stage2Menu;
    public GameObject stage3Menu;

    public TextMeshProUGUI stage1Info;
    public TextMeshProUGUI stage2Info;
    public TextMeshProUGUI stage3Info;

    public Button stage0Button;
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;

    public static int stage;
    public static int enemyCount; // = { 3, 4, 5, 6, 7, 8 };
    public static string[] enemyType; // = { "Bugs", "Boss" };
    public static string battleBg; // = { "Castle", "Castle Gate", "Village", "Forest" };
    public static string quesIndex; // = { "[0-5]", "[6-10]", "[11-15]", "[16-20]", "[21-25]" };

    public static bool tutorialComplete = false;
    public static bool stage1Complete = false;
    public static bool stage2Complete = false;
    public static bool stage3Complete = false;

    public static float stage1Accuracy = 0;
    public static string stage1TotalTime = "-";
    public static float stage2Accuracy = 0;
    public static string stage2TotalTime = "-";
    public static float stage3Accuracy = 0;
    public static string stage3TotalTime = "-";

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

    public void StartLevel()
    {
        if(stage == 0)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    void SetLevelInfo(int stageNumber, string levelBg, string[] type, string qIndex, string topic, float accuracy, string totaltime)
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
            accuracyDisplay = "-";
        }
        else
        {
            accuracyDisplay = accuracy.ToString("F1") + "%";
        }

        //set level info
        if (stageNumber == 0) //tutorial stage
        {
            stageText = "def <b>" + battleBg + "</b>():" +
                            "\n \n     StageInfo = {" +
                            "\n \n          NoEnem:  " + enemyCount +
                            "\n \n          EnemType:  " + enemyType[enemyType.Length - 1] +
                            "\n \n          Topic:  " + topic +
                            "\n \n     }";
        }
        else
        {
            stageText = "def <b>" + battleBg + "</b>():" +
                            "\n \n     StageInfo = {" +
                            "\n \n          NoEnem:  " + enemyCount +
                            "\n \n          EnemType:  " + enemyType[enemyType.Length - 1] +
                            "\n \n          Topic:  " + topic +
                            "\n \n     }" +
                            "\n \n     BestScore = {" +
                            "\n \n          Accuracy:  " + accuracyDisplay +
                            "\n \n          TotalTimeTaken:  " + totaltime +
                            "\n \n     }";
        }
    }

    //map scene to be changed later
    public void Stage0()
    {
        string[] enemType = { "Weak", "Weak" };
        SetLevelInfo(0, "Castle", enemType, "[0-5]", "Python Basics", 0, "-");
        stage1Info.text = stageText;

        if (stage1Menu.activeInHierarchy == false)
        {
            stage1Menu.SetActive(true);
            stage2Menu.SetActive(false);
            stage3Menu.SetActive(false);
        }
        else
            stage1Menu.SetActive(false);
    }

    public void Stage1()
    {
        string[] enemType = { "Weak", "Weak", "Weak" };
        SetLevelInfo(1, "CastleGates", enemType, "[0-5]", "Python Data Types & Operations", stage1Accuracy, stage1TotalTime);
        stage1Info.text = stageText;

        if (stage1Menu.activeInHierarchy == false)
        {
            stage1Menu.SetActive(true);
            stage2Menu.SetActive(false);
            stage3Menu.SetActive(false);
        }
        else
            stage1Menu.SetActive(false);
    }

    public void Stage2()
    {
        string[] enemType = { "Weak", "Weak", "Strong" };
        SetLevelInfo(2, "Village", enemType, "[0-15]", "Python Modules", stage2Accuracy, stage2TotalTime);
        stage2Info.text = stageText;

        if (stage2Menu.activeInHierarchy == false)
        {
            stage1Menu.SetActive(false);
            stage2Menu.SetActive(true);
            stage3Menu.SetActive(false);
        }
        else
            stage2Menu.SetActive(false);
    }

    public void Stage3()
    {
        string[] enemType = { "Strong", "Strong", "Boss" };
        SetLevelInfo(3, "Forest", enemType, "[0-25]", "Python Functions", stage3Accuracy, stage3TotalTime);
        stage3Info.text = stageText;

        if (stage3Menu.activeInHierarchy == false)
        {
            stage1Menu.SetActive(false);
            stage2Menu.SetActive(false);
            stage3Menu.SetActive(true);
        }
        else
            stage3Menu.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject LevelInfo1;
    public GameObject LevelInfo2;
    public GameObject LevelInfo3;

    public TextMeshProUGUI Stage1Info;
    public TextMeshProUGUI Stage2Info;
    public TextMeshProUGUI Stage3Info;

    public static int NoEnemies; // = { 3, 4, 5, 6, 7, 8 };
    public static string EnemType; // = { "Bugs", "Boss" };
    public static string Background; // = { "Castle", "Castle Gate", "Village", "Forest" };
    public static string QuesIndex; // = { "[0-5]", "[6-10]", "[11-15]", "[16-20]", "[21-25]" };

    string StageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(3);
    }

    void SetLevelInfo(string levelBg, int enemyCount, string enemyType, string qIndex)
    {
        Background = levelBg;
        NoEnemies = enemyCount;
        EnemType = enemyType;
        QuesIndex = qIndex;

        StageText = "def <b>" + Background + "</b>():" +
                        "\n \n     StageInfo = {" +
                        "\n \n          NoEnem:  " + NoEnemies +
                        "\n \n          EnemType:  " + EnemType +
                        "\n \n          QnIndex:  " + QuesIndex +
                        "\n \n     };";
    }

    public void whenStage1ButtonClicked()
    {
        SetLevelInfo("CastleGates", 2, "Bugs", "[0-5]");
        Stage1Info.text = StageText;

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
        SetLevelInfo("Village", 2, "Bugs", "[0-15]");
        Stage2Info.text = StageText;

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
        SetLevelInfo("Forest", 3, "Boss", "[0-25]");
        Stage3Info.text = StageText;

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

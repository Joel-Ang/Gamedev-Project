using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    BattleState currentBattleState;
    public enum BattleState
    {
        MCTurn,
        KnightTurn,
        MageTurn,
        PriestTurn,
        EnemyTurn,
        Run,
        Win,
        Lose
    }

    public GameObject mcPlayer;
    public GameObject knightPlayer;
    public GameObject magePlayer;
    public GameObject priestPlayer;
    public GameObject playerTurn;

    public SpriteRenderer bg;
    public Button AttackButton;
    public GameObject enemyWeakPrefab;
    public GameObject enemyStrongPrefab;
    public GameObject enemyBossPrefab;
    public GameObject enemyIndicatorPrefab;
    public GameObject enemyMissPrefab;
    public GameObject enemyEffects;
    public GameObject questionUI;
    public GameObject winUI;
    public TMP_Text accuracyScoreText;
    public TMP_Text timeScoreText;
    public GameObject loseUI;
    public GameObject healthManagerObj;
    HealthManager healthManager;

    public List<GameObject> allPlayers;
    public List<GameObject> allEnemies;

    GameObject currentPlayer; //character of current turn
    int enemyIndex;
    bool choosingEnemy; //whether players are choosing enemy
    float totalTurns = 0;
    float correctTurns = 0;
    float timeTaken = 0f;

    AudioManager audiomanager;

    QuestionMenu questionMenu;
    public TextAsset jsonFile;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnemySpawn();

        //background change
        if (LevelManager.battleBg == "CastleGates")
        {
            bg.sprite = Resources.Load<Sprite>("Backgrounds/BattleBg_CastleGates");
        }
        else if (LevelManager.battleBg == "Village")
        {
            bg.sprite = Resources.Load<Sprite>("Backgrounds/BattleBg_Village");
            bg.gameObject.transform.localScale = new Vector3(1.2f, 1.2f);
        }
        else if (LevelManager.battleBg == "Forest")
        {
            bg.sprite = Resources.Load<Sprite>("Backgrounds/BattleBg_Forest");
        }
        
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void EnemySpawn()
    {
        Transform pos1 = GameObject.Find("EnemyPos1").transform;
        Transform pos2 = GameObject.Find("EnemyPos2").transform;
        Transform pos3 = GameObject.Find("EnemyPos3").transform;

        GameObject enemyToSpawn = null;
        Transform posToSpawn = null;

        if (LevelManager.enemyCount < 2) //1 max
        {
            //set type of enemy
            if (LevelManager.enemyType[0] == "Weak")
            {
                enemyToSpawn = enemyWeakPrefab;
            }
            else if (LevelManager.enemyType[0] == "Strong")
            {
                enemyToSpawn = enemyStrongPrefab;
            }
            else if (LevelManager.enemyType[0] == "Boss")
            {
                enemyToSpawn = enemyBossPrefab;
            }

            //spawn enemy
            GameObject enemy = Instantiate(enemyToSpawn, new Vector3(pos2.position.x, pos2.position.y, pos2.position.z), transform.rotation);
            enemy.transform.parent = pos2;
        }
        else if (LevelManager.enemyCount < 4) //3 max
        {
            foreach (string e in LevelManager.enemyType)
            {
                //set position of enemy
                if (pos1.transform.childCount == 0)
                {
                    posToSpawn = pos1.transform;
                }
                else if (pos3.transform.childCount == 0)
                {
                    posToSpawn = pos3.transform;
                }
                else if (pos2.transform.childCount == 0)
                {
                    posToSpawn = pos2.transform;
                }

                //set type of enemy
                if (e == "Weak")
                {
                    enemyToSpawn = enemyWeakPrefab;
                }
                else if (e == "Strong")
                {
                    enemyToSpawn = enemyStrongPrefab;
                }
                else if (e == "Boss")
                {
                    enemyToSpawn = enemyBossPrefab;
                }

                //spawn enemy
                GameObject enemy = Instantiate(enemyToSpawn, new Vector3(posToSpawn.position.x, posToSpawn.position.y, posToSpawn.position.z), transform.rotation);
                enemy.transform.parent = posToSpawn;
            }
        }
    }

    //testing, to be cleaned later
    public void backtomap()
    {
        SceneManager.LoadScene(2);
    }

    public void retrylevel()
    {
        SceneManager.LoadScene(3);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        allPlayers = new List<GameObject> { mcPlayer, knightPlayer, magePlayer, priestPlayer };
        allEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        //Debug.Log("number of players: " + allPlayers.Count);

        healthManager = healthManagerObj.GetComponent<HealthManager>();
        healthManager.GetEnemyHealth();

        UpdateBattleState(BattleState.MCTurn); //to be run when loading level

        audiomanager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        questionMenu = GameObject.FindGameObjectWithTag("QuestionManager").GetComponent<QuestionMenu>();
        
        //set which question the stage is starts with
        questionMenu.setCurrentTurn(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (choosingEnemy) //if players are choosing enemy
        {
            selectEnemy();
        }
        
        if (questionUI.activeInHierarchy)
        {
            timeTaken += Time.deltaTime;
        }
    }

    public void UpdateBattleState(BattleState nextState)
    {
        currentBattleState = nextState;
        Debug.Log(currentBattleState);

        switch (currentBattleState)
        {
            case BattleState.MCTurn:
                if (allPlayers.Contains(mcPlayer))
                {
                    MCTurn();
                }
                else
                {
                    NextBattleState();
                }
                break;

            case BattleState.KnightTurn:
                if (allPlayers.Contains(knightPlayer))
                {
                    KnightTurn();
                }
                else
                {
                    NextBattleState();
                }
                break;

            case BattleState.MageTurn:
                if (allPlayers.Contains(magePlayer))
                {
                    MageTurn();
                }
                else
                {
                    NextBattleState();
                }
                break;

            case BattleState.PriestTurn:
                if (allPlayers.Contains(priestPlayer))
                {
                    PriestTurn();
                }
                else
                {
                    NextBattleState();
                }
                break;

            case BattleState.EnemyTurn:
                EnemyTurn();
                break;

            case BattleState.Run:
                BattleRun();
                break;

            case BattleState.Win:
                BattleWin();
                break;

            case BattleState.Lose:
                BattleLose();
                break;
        }
    }

    public void NextBattleState()
    {
        UpdateBattleState(currentBattleState += 1);
    }

    void MCTurn()
    {
        currentPlayer = mcPlayer;
        playerTurn.transform.position = new Vector2(mcPlayer.transform.position.x, mcPlayer.transform.position.y - 1f);
        AttackButton.interactable = true;
    }

    void KnightTurn()
    {
        currentPlayer = knightPlayer;
        playerTurn.transform.position = new Vector2(knightPlayer.transform.position.x, knightPlayer.transform.position.y - 0.85f);
        AttackButton.interactable = true;
    }

    void MageTurn()
    {
        currentPlayer = magePlayer;
        playerTurn.transform.position = new Vector2(magePlayer.transform.position.x, magePlayer.transform.position.y - 0.9f);
        AttackButton.interactable = true;
    }

    void PriestTurn()
    {
        currentPlayer = priestPlayer;
        playerTurn.transform.position = new Vector2(priestPlayer.transform.position.x, priestPlayer.transform.position.y - 0.7f);
        AttackButton.interactable = true;
    }

    //when attack button is pressed (players will have to first choose an enemy)
    public void enemySelection()
    {
        if (!choosingEnemy) //if players are not already choosing enemy
        {
            //create indicators above enemies
            foreach (GameObject e in allEnemies)
            {
                GameObject indicator = Instantiate(enemyIndicatorPrefab, new Vector3(e.transform.position.x, e.transform.position.y + 0.9f, e.transform.position.z), transform.rotation);
                indicator.transform.parent = enemyEffects.transform;
            }

            choosingEnemy = true;
            //Debug.Log(choosingEnemy);
        }
    }

    //after selecting an enemy to attack
    void selectEnemy()
    {
        if (Input.GetMouseButtonDown(0)) //if left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 20);

            if (hit && allEnemies.Contains(hit.transform.gameObject)) //if click is on an existing enemy
            {
                //play select sound
                audiomanager.playSelect();
                //destroy indicators
                foreach (Transform i in enemyEffects.transform)
                {
                    Destroy(i.gameObject);
                }

                choosingEnemy = false;
                enemyIndex = allEnemies.IndexOf(hit.transform.gameObject);

                AttackButton.interactable = false;
                currentPlayer.GetComponent<Animator>().SetTrigger("isAttacking");
            }
        }
    }

    public void selectAnswer(int chosenAns)
    {
        questionUI.SetActive(false);

        totalTurns++;
        //check chosen answer         
        bool checkAns = questionMenu.selectAnswer(chosenAns);

        if (checkAns) //correct answer
        {
            correctTurns++;
            //play correct ans and enemy damaged SFX
            audiomanager.playCorrectAns();
            audiomanager.playEnemyDamaged();
            healthManager.ReceiveDamage(healthManager.enemiesHealth[enemyIndex]);
            //set next question
            questionMenu.nextQuestion();
        }
        else //wrong answer
        {
            //miss effect animation
            Transform enemyPos = allEnemies[enemyIndex].transform;
            GameObject missEffect = Instantiate(enemyMissPrefab, new Vector3(enemyPos.position.x, enemyPos.position.y + 1f, enemyPos.position.z), transform.rotation);
            missEffect.transform.parent = enemyEffects.transform;

            //play wrong ans and miss SFX 
            audiomanager.playWrongAns();
            audiomanager.playMiss();
            
            UpdateBattleState(currentBattleState += 1);
        }
    }

    void EnemyTurn()
    {
        playerTurn.SetActive(false);
        AttackButton.interactable = false;

        allEnemies[0].GetComponent<Animator>().SetTrigger("isAttacking");
    }

    public void EnemyAttack()
    {
        //random from remaining players
        int targetPlayerIndex = Random.Range(0, allPlayers.Count-1);
        currentPlayer = allPlayers[targetPlayerIndex];

        //play player damaged SFX
        audiomanager.playPlayerDamaged();

        //deal damage to respective player
        if (currentPlayer == mcPlayer)
        {
            healthManager.ReceiveDamage(healthManager.mcHealth);
            Debug.Log("MC HP: " + healthManager.mcHealth.Health);
        }
        else if (currentPlayer == knightPlayer)
        {
            healthManager.ReceiveDamage(healthManager.knightHealth);
            Debug.Log("Knight HP: " + healthManager.knightHealth.Health);
        }
        else if (currentPlayer == magePlayer)
        {
            healthManager.ReceiveDamage(healthManager.mageHealth);
            Debug.Log("Mage HP: " + healthManager.mageHealth.Health);
        }
        else if (currentPlayer == priestPlayer)
        {
            healthManager.ReceiveDamage(healthManager.priestHealth);
            Debug.Log("Priest HP: " + healthManager.priestHealth.Health);
        }
    }

    public IEnumerator DisplayEndUI(AnimatorStateInfo stateInfo)
    {
        yield return new WaitForSecondsRealtime(stateInfo.length + 1);

        if (allPlayers.Count == 0)
        {
            //play losing SFX and pause BGM
            audiomanager.stopBGM();
            audiomanager.playLose();
            UpdateBattleState(BattleState.Lose);
            Time.timeScale = 0f;
        }
        else if (allEnemies.Count == 0)
        {
            //play winning SFX and pause BGM
            audiomanager.stopBGM();
            audiomanager.playWin();
            UpdateBattleState(BattleState.Win);
            Time.timeScale = 0f;
        }
    }

    public void BattleRun()
    {
        Debug.Log("do you want to escape?");
        backtomap();
    }

    void BattleWin()
    {
        winUI.SetActive(true);

        if (LevelManager.stage == 1)
        {
            LevelManager.stage1Complete = true;
            calculateScore();
        }
        else if (LevelManager.stage == 2)
        {
            LevelManager.stage2Complete = true;
            calculateScore();
        }
        else if (LevelManager.stage == 3)
        {
            LevelManager.stage3Complete = true;
            calculateScore();
        }
    }

    void calculateScore()
    {
        //score calculations
        float accuracyScore = (correctTurns / totalTurns) * 100;

        float minutes = timeTaken / 60;
        float seconds = timeTaken % 60;
        string timeScore;
        if (minutes < 1)
        {
            timeScore = seconds.ToString("N0") + " seconds";
        }
        else
        {
            timeScore = minutes.ToString("N0") + " minutes  " + seconds.ToString("N0") + " seconds";
        }

        //display score in Win UI
        accuracyScoreText.text = accuracyScore.ToString("F1") + "%";
        timeScoreText.text = timeScore;

        //update best score if accuracy increases
        if (LevelManager.stage == 1)
        {
            if (accuracyScore > LevelManager.stage1Accuracy)
            {
                LevelManager.stage1Accuracy = accuracyScore;
                LevelManager.stage1TotalTime = timeScore;
            }
        }
        else if (LevelManager.stage == 2)
        {
            if (accuracyScore > LevelManager.stage2Accuracy)
            {
                LevelManager.stage2Accuracy = accuracyScore;
                LevelManager.stage2TotalTime = timeScore;
            }
        }
        else if (LevelManager.stage == 3)
        {
            if (accuracyScore > LevelManager.stage3Accuracy)
            {
                LevelManager.stage3Accuracy = accuracyScore;
                LevelManager.stage3TotalTime = timeScore;
            }
        }
    }

    void BattleLose()
    {
        loseUI.SetActive(true);
    }


}

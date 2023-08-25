using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public Button AttackButton;
    public GameObject enemyIndicatorPrefab;
    public GameObject enemyIndicators;
    public GameObject questionUI;
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject healthManagerObj;
    HealthManager healthManager;

    GameObject currentPlayer; //character of current turn
    int enemyIndex;
    bool choosingEnemy; //whether players are choosing enemy

    public List<GameObject> allPlayers;
    public List<GameObject> allEnemies;

    AudioManager audiomanager;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        allPlayers = new List<GameObject> { mcPlayer, knightPlayer, magePlayer, priestPlayer };
        allEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        //Debug.Log("number of players: " + allPlayers.Count);

        healthManager = healthManagerObj.GetComponent<HealthManager>();
        healthManager.GetEnemyHealth();

        UpdateBattleState(BattleState.MCTurn); //to be run when loading level

        audiomanager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (choosingEnemy) //if players are choosing enemy
        {
            selectEnemy();
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
        playerTurn.transform.position = new Vector2(knightPlayer.transform.position.x, knightPlayer.transform.position.y - 1f);
        AttackButton.interactable = true;
    }

    void MageTurn()
    {
        currentPlayer = magePlayer;
        playerTurn.transform.position = new Vector2(magePlayer.transform.position.x, magePlayer.transform.position.y - 1f);
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
                indicator.transform.parent = enemyIndicators.transform;
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
                foreach (Transform i in enemyIndicators.transform)
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

    public void selectAnswer()
    {
        questionUI.SetActive(false);

        TMP_Text answerText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>();

        if (answerText.text == "b") //correct answer
        {
            //play correct ans SFX
            audiomanager.playCorrectAns();
            healthManager.ReceiveDamage(healthManager.enemiesHealth[enemyIndex]);
            audiomanager.playEnemyDamaged();
        }
        else //wrong answer
        {
            //play wrong ans and miss SFX 
            audiomanager.playWrongAns();
            audiomanager.playMiss();
            //miss animation wip
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
        //SceneManager.LoadScene(3);
    }

    void BattleWin()
    {
        winUI.SetActive(true);
    }

    void BattleLose()
    {
        loseUI.SetActive(true);
    }
}

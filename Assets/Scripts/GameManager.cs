using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

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

    [Header("--------------------Audio Source--------------------")]
    public AudioSource playSound;
    public AudioClip selectSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    GameObject currentPlayer;
    bool choosingEnemy;

    public List<GameObject> allEnemies;

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
        UpdateBattleState(BattleState.MCTurn);
        allEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                MCTurn();
                break;

            case BattleState.KnightTurn:
                KnightTurn();
                break;

            case BattleState.MageTurn:
                MageTurn();
                break;

            case BattleState.PriestTurn:
                PriestTurn();
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

    void MCTurn()
    {
        currentPlayer = mcPlayer;
        playerTurn.transform.position = new Vector2(mcPlayer.transform.position.x, mcPlayer.transform.position.y - 1f);
        AttackButton.interactable = true;
    }

    void KnightTurn() //test skip turn
    {
        //currentPlayer = knightPlayer;
        //playerTurn.transform.position = new Vector2(knightPlayer.transform.position.x, knightPlayer.transform.position.y - 1f);
        //AttackButton.interactable = true;
        UpdateBattleState(currentBattleState += 1);
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

    public void enemySelection()
    {
        if (!choosingEnemy)
        {
            foreach (GameObject e in allEnemies)
            {
                GameObject indicator = Instantiate(enemyIndicatorPrefab, new Vector3(e.transform.position.x, e.transform.position.y + 0.9f, e.transform.position.z), transform.rotation);
                indicator.transform.parent = enemyIndicators.transform;
            }
        }
        
        choosingEnemy = true;
        //Debug.Log(choosingEnemy);
    }

    void selectEnemy()
    {
        if (choosingEnemy)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 20);

            if (hit && hit.transform.tag == "Enemy")
            {
                playSelectSound();
                Debug.Log(hit.transform.name);
                foreach (Transform i in enemyIndicators.transform)
                {
                    Destroy(i.gameObject);
                }
                choosingEnemy = false;

                AttackButton.interactable = false;
                questionUI.SetActive(true);
            }
        }
    }

    public void selectAnswer()
    {
        playSelectSound();
        TMP_Text answerText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>();

        if (answerText.text == "2") //correct answer
        {
            playCorrectSound();
            currentPlayer.GetComponent<Animator>().SetTrigger("isAttacking");
        }
        else //wrong answer
        {
            playWrongSound();
            UpdateBattleState(currentBattleState += 1);
        }
        questionUI.SetActive(false);
    }

    void EnemyTurn()
    {
        playerTurn.SetActive(false);
        AttackButton.interactable = false;

        allEnemies[0].GetComponent<Animator>().SetTrigger("isAttacking");
    }

    public void EnemyAttack()
    {
        int targetPlayer = Random.Range(0, 4);
        switch(targetPlayer)
        {
            case 0: //MC
                Debug.Log("MC is attacked");
                break;

            case 1: //Knight
                Debug.Log("Knight is attacked");
                break;

            case 2: //Mage
                Debug.Log("Mage is attacked");
                break;

            case 3: //Priest
                Debug.Log("Priest is attacked");
                break;
        }
    }

    public void BattleRun()
    {
        Debug.Log("do you want to escape?");
    }

    void BattleWin()
    {
        Debug.Log("you won the battle");
    }

    void BattleLose()
    {
        Debug.Log("you lost the battle");
    }
    public void playSelectSound(){
        playSound.clip = selectSound;
        playSound.Play();
    }
    public void playCorrectSound(){
        playSound.clip = correctSound;
        playSound.Play();
    }
    public void playWrongSound(){
        playSound.clip = wrongSound;
        playSound.Play();
    }
    
}

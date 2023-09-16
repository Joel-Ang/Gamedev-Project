using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public HealthSystem mcHealth;
    public HealthSystem knightHealth;
    public HealthSystem mageHealth;
    public HealthSystem priestHealth;


    public GameObject mcHealthObj;
    public GameObject knightHealthObj;
    public GameObject mageHealthObj;
    public GameObject priestHealthObj;
    
    public List<HealthSystem> enemiesHealth = new List<HealthSystem>();
    GameObject enemyhealthObj;

    //public GameObject healthPrefab;

    // Start is called before the first frame update
    void Start()
    {
        mcHealth = new HealthSystem(GameManager.instance.mcPlayer, 4);
        knightHealth = new HealthSystem(GameManager.instance.knightPlayer, 4);
        mageHealth = new HealthSystem(GameManager.instance.magePlayer, 4);
        priestHealth = new HealthSystem(GameManager.instance.priestPlayer, 4);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetEnemyHealth()
    {
        foreach (GameObject e in GameManager.instance.allEnemies)
        {
            if (e.name == "EnemyWeak")
            {
                enemiesHealth.Add(new HealthSystem(e, 2));
            }
            else if (e.name == "EnemyStrong")
            {
                enemiesHealth.Add(new HealthSystem(e, 3));
            }
            else if (e.name == "Boss")
            {
                enemiesHealth.Add(new HealthSystem(e, 5));
            }
        }
        Debug.Log(enemiesHealth.Count);
    }

    public void ReceiveDamage(HealthSystem targetHealth)
    {
        targetHealth.Character.GetComponent<Animator>().SetTrigger("isDamaged");
        //Debug.Log("damage");
        targetHealth.Health -= 1;
        UpdateHealthBar(targetHealth);
        CheckDeath(targetHealth);
    }

    void UpdateHealthBar(HealthSystem healthToUpdate)
    {
        if (healthToUpdate == mcHealth)
        {
            if (mcHealthObj.transform.childCount > 0)
            {
                Destroy(mcHealthObj.transform.GetChild(mcHealthObj.transform.childCount - 1).gameObject);
            }
        }
        else if (healthToUpdate == knightHealth)
        {
            if (knightHealthObj.transform.childCount > 0)
            {
                Destroy(knightHealthObj.transform.GetChild(knightHealthObj.transform.childCount - 1).gameObject);
            }
        }
        else if (healthToUpdate == mageHealth)
        {
            if (mageHealthObj.transform.childCount > 0)
            {
                Destroy(mageHealthObj.transform.GetChild(mageHealthObj.transform.childCount - 1).gameObject);
            }
        }
        else if (healthToUpdate == priestHealth)
        {
            if (priestHealthObj.transform.childCount > 0)
            {
                Destroy(priestHealthObj.transform.GetChild(priestHealthObj.transform.childCount - 1).gameObject);
            }
        }
        else if (healthToUpdate.Character.tag == "Enemy")
        {
            enemyhealthObj = healthToUpdate.Character.transform.parent.GetChild(1).GetChild(0).gameObject;
            if (enemyhealthObj.transform.childCount > 0)
            {
                Destroy(enemyhealthObj.transform.GetChild(enemyhealthObj.transform.childCount - 1).gameObject);
            }
        }
    }

    void CheckDeath(HealthSystem healthToCheck)
    {
        if (healthToCheck.Health == 0)
        {
            healthToCheck.Character.GetComponent<Animator>().SetTrigger("isDead");
            if (healthToCheck.Character.tag == "Player")
            {
                GameManager.instance.allPlayers.Remove(healthToCheck.Character);
            }
            else if (healthToCheck.Character.tag == "Enemy")
            {
                AudioManager.instance.playEnemyDeath();
                GameManager.instance.allEnemies.Remove(healthToCheck.Character);
                enemiesHealth.Remove(healthToCheck);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    bool inTutorial;
    
    public GameObject tutorial1; //click attack button
    public GameObject tutorial2; //select enemy
    public GameObject tutorial3; //answer question
    public GameObject tutorial4; //end of tutorial

    public Transform optionButtons;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AttackButton.onClick.AddListener(ContinueTutorial); //to end tutorial 1
        foreach (Transform option in optionButtons)
        {
            option.GetComponent<Button>().onClick.AddListener(ContinueTutorial); //to end tutorial 3 (part 2/2)
        }

        inTutorial = true;
        GameManager.instance.AttackButton.interactable = false;
        StartCoroutine(StartTutorial(tutorial1, 1.5f)); //start first tutorial
    }

    // Update is called once per frame
    void Update()
    {
        //to end tutorial 2
        if (tutorial2.activeInHierarchy && !GameManager.instance.choosingEnemy)
        {
            ContinueTutorial();
        }

        //to end tutorial 3 (part 1/2)
        if (tutorial3.transform.GetChild(2).gameObject.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            tutorial3.transform.GetChild(2).gameObject.SetActive(false); //dialogue box
            tutorial3.transform.GetChild(1).gameObject.SetActive(false); //panel bg
        }

        //to end tutorial 4
        if (tutorial4.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            ContinueTutorial();
        }
    }

    void ContinueTutorial()
    {
        if (inTutorial)
        {
            Time.timeScale = 1f;
            if (tutorial1.activeInHierarchy)
            {
                tutorial1.SetActive(false);
                StartCoroutine(StartTutorial(tutorial2, 1));
            }
            if (tutorial2.activeInHierarchy)
            {
                tutorial2.SetActive(false);
                StartCoroutine(StartTutorial(tutorial3, 2));
            }
            if (tutorial3.activeInHierarchy)
            {
                tutorial3.SetActive(false);
                StartCoroutine(StartTutorial(tutorial4, 1));
            }
            if (tutorial4.activeInHierarchy)
            {
                tutorial4.SetActive(false);
                inTutorial = false;
            }
        }
    }

    public IEnumerator StartTutorial(GameObject tutorial, float secondsToWait)
    {
        yield return new WaitForSecondsRealtime(secondsToWait);

        Time.timeScale = 0f;
        tutorial.SetActive(true);
        if (tutorial == tutorial1)
        {
            GameManager.instance.AttackButton.interactable = true;
        }
    }
}

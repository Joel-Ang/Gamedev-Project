using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class QuestionMenu : MonoBehaviour
{
    public TMP_Text answer1;
    public TMP_Text answer2;
    public TMP_Text answer3;
    public TMP_Text answer4;

    public GameObject questionUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectAnswer()
    {
        TMP_Text answerText = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_Text>();
        if (answerText.text == "2")
        {
            Debug.Log("correct answer");
        }
        else
        {
            Debug.Log("wrong answer");
        }
        questionUI.SetActive(false);
    }
}

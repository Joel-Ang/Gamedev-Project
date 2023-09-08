using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class QuestionMenu : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text optionsText;
    public TextAsset jsonFile;
    public Questions questionJsonText;
    public int currentTurn = 0; 
    // Start is called before the first frame update
    void Start()
    {
        JSONReader();
        setQuestionText(currentTurn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkAnswer(int chosenAns)
    {
        if (chosenAns == questionJsonText.questions[currentTurn].ans)
        {
            Debug.Log("correct answer");
            return true;
        }
        else
        {
            Debug.Log("wrong answer");
            return false;
        }
    }
    public void setQuestionText(int turn)
    {
        //set question text 
        questionText.text = questionJsonText.questions[turn].qn;

        //set answers text
        string choiceStr =  "a. " + questionJsonText.questions[turn].choice[0] + "\n" +
                            "b. " + questionJsonText.questions[turn].choice[1] + "\n" +
                            "c. " + questionJsonText.questions[turn].choice[2] + "\n" +
                            "d. " + questionJsonText.questions[turn].choice[3] + "\n";
        optionsText.text = choiceStr;
    }

    public void nextQuestion()
    {
        //increment currentTurn by one and if it is 25 set it to 0 
        currentTurn++;
        if (currentTurn == 25)
        {
            currentTurn = 0;
        }
        setQuestionText(currentTurn);
    }
    public void JSONReader()
    {
        questionJsonText = JsonUtility.FromJson<Questions>(jsonFile.text);
    }
    public void setCurrentTurn(int turn)
    {
        currentTurn = turn;
    }
}

[System.Serializable]
public class Question
{
    public string qn; 
    public List<string> choice = new List<string>();
    public int qnNumber;
    public int ans;
}

[System.Serializable]
public class Questions
{
    public Question[] questions;
}
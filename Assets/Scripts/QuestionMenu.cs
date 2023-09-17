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
    List<int> tutorial = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
    List<int> stage1 = new List<int> { 8, 9, 10, 11, 12, 13, 14 };
    List<int> stage2 = new List<int> { 15, 16, 17, 18, 19, 20, 21, 22, 23 };
    List<int> stage3 = new List<int> { 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 };
    List<int> currStageQn;

    // Start is called before the first frame update
    void Start()
    {
        JSONReader();
        if (currentTurn == 0) { currStageQn = tutorial; };
        if (currentTurn == 8) { currStageQn = stage1; };
        if (currentTurn == 15) { currStageQn = stage2; };
        if (currentTurn == 24) { currStageQn = stage3; };
        nextQuestion();
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
            //remove current question number from currStageQn
            currStageQn.RemoveAll(x => x == currentTurn);
            //Debug.Log("correct answer");
            return true;
        }
        else
        {
            //Debug.Log("wrong answer");
            return false;
        }
    }
    public void setQuestionText(int turn)
    {
        //set question text 
        questionText.text = questionJsonText.questions[turn].qn;

        //set answers text
        string choiceStr = "a. " + questionJsonText.questions[turn].choice[0] + "\n" +
                            "b. " + questionJsonText.questions[turn].choice[1] + "\n" +
                            "c. " + questionJsonText.questions[turn].choice[2] + "\n" +
                            "d. " + questionJsonText.questions[turn].choice[3] + "\n";
        optionsText.text = choiceStr;
    }

    public void nextQuestion()
    {
        //if currStageQn is not empty choose random question from list else set currentTurn to 0 
        if (currStageQn.Count != 0)
        {
            currentTurn = currStageQn[Random.Range(0, currStageQn.Count)];
        }
        else
        {
            currentTurn = 0;
        }
        //Debug.Log("Choosen random question is " + currentTurn);
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
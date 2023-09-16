using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class DialogueManager : MonoBehaviour
{
    //public static DialogueManager instance;
    
    TypewriterEffect typewriterScript;
    public GameObject dialogueBoxObj;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image speakerImage;
    //[SerializeField] GameObject[] canTalk;
    public PlayableDirector timelinePlayable;

    Queue<Dialogues.Character> allSpeakers;
    Queue<string> allDialogues;

    Dialogues.Character currentSpeaker;
    bool isStarting = false; //check if first line
    bool hideDialogue = false;
    bool isTalking = false; //check for dialogue pause
    string stopBeforeDialogue = ""; //dialogue to stop before
    bool waitForAnim; //make true if animation needs to finish before next line starts

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        typewriterScript = GetComponent<TypewriterEffect>();
        typewriterScript.txt = dialogueText;
        allSpeakers = new Queue<Dialogues.Character>();
        allDialogues = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentSpeaker)
        {
            case Dialogues.Character.King:
                speakerImage.sprite = Resources.Load<Sprite>("Dialogue_King");
                nameText.text = currentSpeaker.ToString();
                break;

            case Dialogues.Character.MC:
                speakerImage.sprite = Resources.Load<Sprite>("Dialogue_MC");
                nameText.text = currentSpeaker.ToString();
                break;

            case Dialogues.Character.Sorcerer:
                speakerImage.sprite = Resources.Load<Sprite>("Dialogue_Sorcerer");
                nameText.text = currentSpeaker.ToString();
                break;

            case Dialogues.Character.Boss:
                speakerImage.sprite = Resources.Load<Sprite>("Dialogue_Boss");
                nameText.text = currentSpeaker.ToString();
                break;

            case Dialogues.Character.Senpai:
                speakerImage.sprite = Resources.Load<Sprite>("Dialogue_Senpai");
                nameText.text = currentSpeaker.ToString();
                break;
        }
    }

    public void getDialogues(GameObject theSpeaker)
    {
        foreach (Dialogues.Dialogue d in theSpeaker.GetComponent<Dialogues>().dialogue)
        {
            allSpeakers.Enqueue(d.character);
            allDialogues.Enqueue(d.content);
        }
    }

    public void StartDialogue()
    {
        isTalking = true;
        StartCoroutine(DisplayDialogue());
    }

    public void StopDialogue(string before)
    {
        //timelinePlayable.Pause();
        isTalking = false;
        stopBeforeDialogue = before;
        hideDialogue = false;
    }
    public void StopAndCloseDialogue(string before)
    {
        //timelinePlayable.Pause();
        isTalking = false;
        stopBeforeDialogue = before;
        hideDialogue = true;
    }

    public void CheckAnimation(bool waiting)
    {
        waitForAnim = waiting;
        if (waitForAnim == false)
        {
            timelinePlayable.Pause();
        }
    }

    public void PlayTimeline()
    {
        timelinePlayable.Play();
    }

    IEnumerator DisplayDialogue()
    {
        dialogueBoxObj.SetActive(true);
        isStarting = true;

        while (allDialogues.Count > 0)
        {
            //go to next line (if end of line and spacebar is pressed OR if starting line)
            if ((typewriterScript.isFinishedTyping && Input.GetKeyDown(KeyCode.Space)) || isStarting)
            {
                if (isStarting)
                {
                    isStarting = false;
                }

                //replace if dialogue is empty
                if (allDialogues.Peek() == "")
                {
                    currentSpeaker = allSpeakers.Dequeue();
                    yield return StartCoroutine(typewriterScript.TypeText("..."));
                    allDialogues.Dequeue();
                }
                //if next line is a pause
                else if (allDialogues.Peek() == stopBeforeDialogue && !isTalking)
                {
                    timelinePlayable.Play();
                    isTalking = true;
                    if (hideDialogue)
                    {
                        dialogueBoxObj.SetActive(false);
                    }
                    yield break;
                }
                //type text if no animations to wait for
                else if (!waitForAnim)
                {
                    currentSpeaker = allSpeakers.Dequeue();
                    yield return StartCoroutine(typewriterScript.TypeText(allDialogues.Dequeue()));
                    //Debug.Log("nth");
                }
            }
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        //Debug.Log("story end");
        timelinePlayable.Play();
    }

    public void DisplayNextSceneButton(GameObject buttonObj)
    {
        buttonObj.SetActive(true);
    }
}

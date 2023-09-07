using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    TypewriterEffect typewriterScript;
    public GameObject dialogueBoxObj;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image speakerImage;
    //[SerializeField] GameObject[] canTalk;

    Queue<Dialogues.Character> allSpeakers;
    Queue<string> allDialogues;

    Dialogues.Character currentSpeaker;
    bool isTalking = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        typewriterScript = GetComponent<TypewriterEffect>();
        typewriterScript.txt = dialogueText;
        allSpeakers = new Queue<Dialogues.Character>();
        allDialogues = new Queue<string>();

        //getDialogues();
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return) && isTalking == false)
        //{
        //    StartDialogue();
        //}
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

    public void EndDialogue()
    {
        isTalking = false;
    }

    IEnumerator DisplayDialogue()
    {
        dialogueBoxObj.SetActive(true);

        for (int i = 0; i < allDialogues.Count;)
        {
            currentSpeaker = allSpeakers.Dequeue();
            
            if (allDialogues.Peek() == "")
            {
                yield return StartCoroutine(typewriterScript.TypeText("I have nothing to say :((("));
                allDialogues.Dequeue();
            }
            else
            {
                yield return StartCoroutine(typewriterScript.TypeText(allDialogues.Dequeue()));
                //Debug.Log("nth");
            }

            if (isTalking == false)
            {
                yield return new WaitForSeconds(0.5f);
                dialogueBoxObj.SetActive(false);
                yield break;
            }
        }
        
        dialogueBoxObj.SetActive(false);
    }
}

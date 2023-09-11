using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text txt;
    public bool isFinishedTyping = true;
    bool skip = false;
    [SerializeField] float typingSpeed = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skip = true;
        }
    }

    public IEnumerator TypeText(string content)
    {
        skip = false; //to only allow skip after typing starts
        isFinishedTyping = false;
        txt.text = "";

        foreach (char c in content)
        {
            //AudioManager.instance.playTextTyping();
            if (skip)
            {
                txt.text = content;
                break;
            }
            else
            {
                txt.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        skip = false;
        isFinishedTyping = true;
    }
}

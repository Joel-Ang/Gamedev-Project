using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text txt;
    [SerializeField] float typingSpeed = 0.05f;
    [SerializeField] bool allowSkip = false;
    
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMP_Text>();
        //StartCoroutine(TypeText(txt.text));
    }

    public IEnumerator TypeText(string content)
    {
        txt.text = "";

        foreach (char c in content)
        {
            AudioManager.instance.playTextTyping();
            if (allowSkip == true && Input.GetKeyDown(KeyCode.Space))
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

        yield return new WaitForSeconds(1f);
    }
}

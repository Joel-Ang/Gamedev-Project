using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    TMP_Text txt;
    public float typingSpeed = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMP_Text>();
        StartCoroutine(TypeText(txt.text));
    }

    IEnumerator TypeText(string content)
    {
        txt.text = "";
        foreach (char c in content)
        {
            txt.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}

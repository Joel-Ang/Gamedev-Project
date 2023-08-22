using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Dialogues : MonoBehaviour
{
    public Dialogue[] dialogue;

    [System.Serializable]
    public class Dialogue
    {
        public Character character;
        [TextArea] public string content;
    }

    public enum Character
    {
        King,
        MC,
        Guard
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapScene()
    {
        audioManager.playMapBGM();
        SceneManager.LoadScene(2);
    }

    public void Stage1()
    {
        SceneManager.LoadScene(3);
    }
}

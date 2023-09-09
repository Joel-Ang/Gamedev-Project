using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    public AudioClip titleBGM;
    public AudioClip prologueBGM;
    public AudioClip mapBGM;
    public AudioClip battleBGM;
    public AudioClip selectSFX;
    public AudioClip correctAnsSFX;
    public AudioClip wrongAnsSFX;
    public AudioClip playerDamagedSFX;
    public AudioClip enemyDamagedSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip missSFX;
    public AudioClip enemyDeathSFX;
    public AudioClip playerDeathSFX;
    public AudioClip textTypingSFX;
    Button[] buttons;
    // Start is called before the first frame update

    void Start()
    {
        GameObject[] audioManager = GameObject.FindGameObjectsWithTag("AudioManager");
        DontDestroyOnLoad(this.gameObject);
        playTitleBGM();
        SceneManager.sceneLoaded += OnSceneLoaded;
        buttons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => playSelect());
        }

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void playSelect()
    {
        sfxAudioSource.PlayOneShot(selectSFX, 1f);
    }
    public void playCorrectAns()
    {
        sfxAudioSource.PlayOneShot(correctAnsSFX, 1f);
    }
    public void playWrongAns()
    {
        sfxAudioSource.PlayOneShot(wrongAnsSFX, 1f);
    }
    public void playPlayerDamaged()
    {
        sfxAudioSource.PlayOneShot(playerDamagedSFX, 1f);
    }
    public void playEnemyDamaged()
    {
        sfxAudioSource.PlayOneShot(enemyDamagedSFX, 1f);
    }
    public void playWin()
    {
        sfxAudioSource.PlayOneShot(winSFX, 1f);
    }
    public void playLose()
    {
        sfxAudioSource.PlayOneShot(loseSFX, 1f);
    }
    public void playMiss()
    {
        sfxAudioSource.PlayOneShot(missSFX, 3f);
    }
    public void playEnemyDeath()
    {
        sfxAudioSource.PlayOneShot(enemyDeathSFX, 1f);
    }
    public void playTextTyping()
    {
        sfxAudioSource.PlayOneShot(textTypingSFX, 1f);
    }
    public void stopBGM()
    {
        sfxAudioSource.Stop();
        bgmAudioSource.Stop();
    }
    public void playTitleBGM()
    {
        stopBGM();
        bgmAudioSource.clip = titleBGM;
        bgmAudioSource.volume = 0.3f;
        bgmAudioSource.pitch = 1.0f;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
    public void playPrologueBGM()
    {
        stopBGM();
        bgmAudioSource.clip = prologueBGM;
        bgmAudioSource.volume = 0.3f;
        sfxAudioSource.pitch = 1.4f;
        bgmAudioSource.pitch = 0.45f;
        bgmAudioSource.loop = false;
        bgmAudioSource.Play();
    }
    public void playMapBGM()
    {
        stopBGM();
        bgmAudioSource.clip = mapBGM;
        bgmAudioSource.volume = 0.3f;
        sfxAudioSource.pitch = 1.0f;
        bgmAudioSource.pitch = 1.0f;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
    public void playBattleBGM()
    {
        stopBGM();
        bgmAudioSource.clip = battleBGM;
        bgmAudioSource.volume = 0.3f;
        bgmAudioSource.pitch = 1.0f;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        buttons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => playSelect());
        }
        string sceneName = scene.name;
        Debug.Log(sceneName);
        if (sceneName == "Prologue")
        {
            playPrologueBGM();
        }
        if (sceneName == "Map")
        {
            playMapBGM();
        }
        if (sceneName == "Battle" || sceneName == "Tutorial")
        {
            playBattleBGM();
        }
    }
}
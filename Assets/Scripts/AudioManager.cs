using System.Collections;
using System.Collections.Generic;
using System;
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
    public Button[] buttons;
    float bgmVolume = 0.3f;
    float sfxVolume = 1f;
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
        sfxAudioSource.PlayOneShot(selectSFX, sfxVolume);
    }
    public void playCorrectAns()
    {
        sfxAudioSource.PlayOneShot(correctAnsSFX, sfxVolume);
    }
    public void playWrongAns()
    {
        sfxAudioSource.PlayOneShot(wrongAnsSFX, sfxVolume);
    }
    public void playPlayerDamaged()
    {
        sfxAudioSource.PlayOneShot(playerDamagedSFX, sfxVolume);
    }
    public void playEnemyDamaged()
    {
        sfxAudioSource.PlayOneShot(enemyDamagedSFX, sfxVolume);
    }
    public void playWin()
    {
        sfxAudioSource.PlayOneShot(winSFX, sfxVolume);
    }
    public void playLose()
    {
        sfxAudioSource.PlayOneShot(loseSFX, sfxVolume);
    }
    public void playMiss()
    {
        sfxAudioSource.PlayOneShot(missSFX, sfxVolume);
    }
    public void playEnemyDeath()
    {
        sfxAudioSource.PlayOneShot(enemyDeathSFX, sfxVolume);
    }
    public void playTextTyping()
    {
        sfxAudioSource.PlayOneShot(textTypingSFX, sfxVolume);
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
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.pitch = 1.0f;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
    public void playPrologueBGM()
    {
        stopBGM();
        bgmAudioSource.clip = prologueBGM;
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.pitch = 0.45f;
        bgmAudioSource.loop = false;
        bgmAudioSource.Play();
    }
    public void playMapBGM()
    {
        stopBGM();
        bgmAudioSource.clip = mapBGM;
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.pitch = 1.0f;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
    public void playBattleBGM()
    {
        stopBGM();
        bgmAudioSource.clip = battleBGM;
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.pitch = 1.0f;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        buttons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (var button in buttons)
        {
            //To stop more onclick listeners from being added to the setting buttons
            if (button.name == "SettingOpenButton" ||
                button.name == "SettingCloseButton") 
            {
                Debug.Log("Skipped " + button.name);
                continue; 
            }
            button.onClick.RemoveListener(playSelect);
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
    public void bgmVolumeChange(float vol)
    {
        bgmVolume = vol;
        bgmAudioSource.volume = bgmVolume;
    }
    public void sfxVolumeChange(float vol)
    {
        sfxVolume = vol;
        sfxAudioSource.volume = sfxVolume;
    }
}
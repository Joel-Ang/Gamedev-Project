using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;
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

    // Start is called before the first frame update
    
    void Start()
    {
        GameObject[] audioManager = GameObject.FindGameObjectsWithTag("AudioManager");
        DontDestroyOnLoad(this.gameObject);
        playTitleBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSelect(){
        audioSource.PlayOneShot(selectSFX,1f);
    }
    public void playCorrectAns(){
        audioSource.PlayOneShot(correctAnsSFX,1f);
    }
    public void playWrongAns(){
        audioSource.PlayOneShot(wrongAnsSFX,1f);
    }
    public void playPlayerDamaged(){
        audioSource.PlayOneShot(playerDamagedSFX,1f);
    }
    public void playEnemyDamaged(){
        audioSource.PlayOneShot(enemyDamagedSFX,1f);
    }
    public void playWin(){
        audioSource.PlayOneShot(winSFX,1f);
    }
    public void playLose(){
        audioSource.PlayOneShot(loseSFX,1f);
    }
    public void playMiss(){
        audioSource.PlayOneShot(missSFX,3f);
    }
    public void playEnemyDeath(){
        audioSource.PlayOneShot(enemyDeathSFX,1f);
    }
    public void stopBGM(){
        audioSource.Stop();
    }
    public void playTitleBGM(){
        stopBGM();
        audioSource.clip = titleBGM;
        audioSource.volume = 0.3f;
        audioSource.pitch = 1.0f;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void playPrologueBGM(){
        stopBGM();
        playSelect();
        audioSource.clip = prologueBGM;
        audioSource.volume = 0.3f;
        audioSource.pitch = 0.45f;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void playMapBGM(){
        stopBGM();
        audioSource.clip = mapBGM;
        audioSource.volume = 0.3f;
        audioSource.pitch = 1.0f;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void playBattleBGM(){
        stopBGM();
        audioSource.clip = battleBGM;
        audioSource.volume = 0.3f;
        audioSource.pitch = 1.0f;
        audioSource.loop = true;
        audioSource.Play();
    }
}

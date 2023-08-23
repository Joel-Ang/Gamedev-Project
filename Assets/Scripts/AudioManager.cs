using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource bgm;
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
        playBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSelect(){
        bgm.PlayOneShot(selectSFX,1f);
    }
    public void playCorrectAns(){
        bgm.PlayOneShot(correctAnsSFX,1f);
    }
    public void playWrongAns(){
        bgm.PlayOneShot(wrongAnsSFX,1f);
    }
    public void playPlayerDamaged(){
        bgm.PlayOneShot(playerDamagedSFX,1f);
    }
    public void playEnemyDamaged(){
        bgm.PlayOneShot(enemyDamagedSFX,1f);
    }
    public void playWin(){
        bgm.PlayOneShot(winSFX,1f);
    }
    public void playLose(){
        bgm.PlayOneShot(loseSFX,1f);
    }
    public void playMiss(){
        bgm.PlayOneShot(missSFX,3f);
    }
    public void playEnemyDeath(){
        bgm.PlayOneShot(enemyDeathSFX,1f);
    }
    public void stopBGM(){
        bgm.Stop();
    }
    public void playBGM(){
        bgm.Play();
    }
}

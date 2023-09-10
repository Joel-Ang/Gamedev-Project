using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public GameObject settingMenu;
    GameObject settingCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] settingManager = GameObject.FindGameObjectsWithTag("SettingManager");
        DontDestroyOnLoad(this.gameObject);
        settingCanvas = settingMenu.transform.parent.gameObject;
        DontDestroyOnLoad(settingCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openSetting()
    {
        settingMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void closeSetting()
    {
        settingMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void bgmVolumeChange(float volume)
    {
        AudioManager.instance.bgmVolumeChange(volume);
    }
    public void sfxVolumeChange(float volume)
    {
        AudioManager.instance.sfxVolumeChange(volume);
    }
    public void playSelect()
    {
        AudioManager.instance.playSelect();
    }
}

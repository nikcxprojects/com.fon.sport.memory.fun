using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    private int volumeSounds;
    private int volumeMusic;
    private int vibration;
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    [SerializeField] private Text text3;
    
    void Start()
    {
        volumeSounds = PlayerPrefs.GetInt("VolumeSounds", 1);
        volumeMusic = PlayerPrefs.GetInt("VolumeMusic", 1);
        UpdateUI();
    }

    public void ChangeSoundsVolume()
    {
        volumeSounds = volumeSounds == 0 ? 1 : 0;
        PlayerPrefs.SetInt("VolumeSounds", volumeSounds);
        UpdateUI();
    }
    
    public void ChangeVipration()
    {
        vibration = vibration == 0 ? 1 : 0;
        PlayerPrefs.SetInt("Vibration", vibration);
        UpdateUI();
    }
    
    public void ChangeMusicVolume()
    {
        volumeMusic = volumeMusic == 0 ? 1 : 0;
        PlayerPrefs.SetInt("VolumeMusic", volumeMusic);
        UpdateUI();
    }

    private void UpdateUI()
    {
        text.text = volumeSounds == 0 ? "SOUNDS OFF" : "SOUNDS ON";
        text2.text = volumeMusic == 0 ? "MUSIC OFF" : "MUSIC ON";
        text3.text = vibration == 0 ? "VIBRA OFF" : "VIBRA ON";
    }
    
}

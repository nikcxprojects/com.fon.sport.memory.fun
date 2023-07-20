using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{

    public Sprite off;
    public Sprite on;

    private Image _image;
    private bool  active;
    
    void OnEnable()
    {
        _image = GetComponent<Image>();
        var sound = PlayerPrefs.GetInt("Sound", 1);
        active = sound == 1;
        SetSound(active);
        EnableSounds(active);
    }

    private void SetSound(bool val)
    {
        if (val) _image.sprite = on;
        else _image.sprite = off;
    }

    public void SwitchSound()
    {
        active = !active;
        SetSound(active);
        EnableSounds(active);
        if(active) PlayerPrefs.SetInt("Sound", 1);
        else PlayerPrefs.SetInt("Sound", 0);
    }
    
    private void EnableSounds(bool val)
    {
        
        var audios = Resources.FindObjectsOfTypeAll<AudioSource>();
        foreach (var a in audios)
        {
            if(!val) a.volume = 0;
            else a.volume = 1;
        }
    }

}

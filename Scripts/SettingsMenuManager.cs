using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public Slider MasterVol;
    public Slider MusicVol;
    public Slider SFXVol;
    public AudioMixer AudioMixer;

    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        AudioMixer.SetFloat("Master Volume", MasterVol.value);
    }

    public void ChangeMusicVolume()
    {
        AudioMixer.SetFloat("Music Volume", MusicVol.value);
    }

    public void ChangeSFXVolume()
    {
        AudioMixer.SetFloat("SFX Volume", SFXVol.value);
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

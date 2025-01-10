using UnityEngine;
using UnityEngine.Audio;

public class VolumeAdjuster : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
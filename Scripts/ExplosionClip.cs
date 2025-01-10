using UnityEngine;

public class ExplosionClip : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip explosionSound;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayExplosionSound()
    {
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }
}
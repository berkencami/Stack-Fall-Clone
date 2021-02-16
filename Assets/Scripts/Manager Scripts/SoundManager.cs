using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    public bool sound = true;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(AudioClip clip, float volume)
    {
    
        if (sound)
            audioSource.PlayOneShot(clip, volume);

       
    }

    public void SoundOnOff()
    {
        sound = !sound;
    }
}

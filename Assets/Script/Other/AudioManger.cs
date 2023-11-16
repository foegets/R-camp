using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManger : MonoBehaviour
{
    public static AudioManger Instance;
    public Sound[]MusicSounds, SfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        PlayMusic("Theme");  
    }
    public void PlayMusic(string name)
    {
        Sound s=Array.Find(MusicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
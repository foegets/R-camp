using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletAudioManager : MonoBehaviour
{
    public AudioSource FireFXsoure;
    public AudioSource HitFXsoure;
    // Start is called before the first frame update

    private void Awake()
    {
        FireFXsoure = GetComponent<AudioSource>();
        HitFXsoure = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFireFXsoure()
    {
        FireFXsoure.Play();
    }

    public void PlayHitFXsoure() 
    {
        HitFXsoure.Play();
    }
}

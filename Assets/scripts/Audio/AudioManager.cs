using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource FXsouce;
    // Start is called before the first frame update

    private void Awake()
    {
        FXsouce = GetComponent<AudioSource>();
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFXsoure()
    {
        FXsouce.Play();
        Debug.Log("FXsoure");
    }
}

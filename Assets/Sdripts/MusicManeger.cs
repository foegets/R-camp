using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManeger : MonoBehaviour
{
    public static MusicManeger instance;  // µ¿˝

    public AudioSource[] sources;//“Ù¿÷«Âµ•

    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int x)
    {
        sources[x].Play();
    }
}

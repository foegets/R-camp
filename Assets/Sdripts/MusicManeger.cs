using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManeger : MonoBehaviour
{
    public static MusicManeger instance;  //ʵ��

    public AudioSource[] sources;//�����嵥

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

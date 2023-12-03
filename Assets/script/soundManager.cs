using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static AudioClip playerAttack;
    public static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerAttack = Resources.Load<AudioClip>("Bow2");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void playerattack()//在静态函数里不可以调用非静态变量，所以前面要变成静态变量
    {
        audioSource.PlayOneShot(playerAttack);
    }
}

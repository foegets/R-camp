using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public  static AudioClip playerAttack;
    private  static AudioSource audioSource;
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
    public static void playerattack()
    {
        audioSource.PlayOneShot(playerAttack);
    }
}

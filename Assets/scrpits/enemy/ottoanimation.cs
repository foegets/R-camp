using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ottoanimation : MonoBehaviour
{
    private Animator anim;
    private character character;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<character>();
    }
    private void Update()
    {
        if (character.currenthealth <= 0)
            anim.SetBool("isdead", true);
        else
            anim.SetBool("isdead", false);
    }
}

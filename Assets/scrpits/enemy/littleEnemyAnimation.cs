using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleEnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private littleEnemy littleEnemy;
    private character character;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        littleEnemy = GetComponent<littleEnemy>();
        character=GetComponent<character>();
    }

    private void Update()
    {
        if (littleEnemy.hurttime >= 0)
            anim.SetBool("ishurt", true);
        else
            anim.SetBool("ishurt", false);
        if (character.currenthealth <= 0)
            anim.SetBool("isdead", true);
        else
            anim.SetBool("isdead", false);

    }
}

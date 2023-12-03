using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;

    public float attackRange;

    public float attackRate;

    public float knockBack;

    public Character character;

    private void Awake()
    {
        character = gameObject.GetComponent<Character>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if(character != null&&other.tag == "Player" )
            if(character.isdead != true)
                other.GetComponent<Character>()?.TakeDamage(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Defense : MonoBehaviour
{
    public Attack attack;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>()?.isAttacking == true )
        {
            collision.GetComponent<Character>().TakeDamage(attack);
        }
        else
        {
            return;
        }
    }
        

}

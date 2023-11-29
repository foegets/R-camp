using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    private Collider2D headCollider; // Í·²¿Åö×²Æ÷
    void Start()
    {
        Transform head = transform.Find("DeathPoint");
        if (head != null)
        {
            Debug.Log("ÕÒµ½Head");
            Collider2D headCollider = GetComponent<Collider2D>();
        }
        
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var script = GetComponent<FSM>();
        if (collision.gameObject.CompareTag("Foot") && collision.collider == headCollider)
        {
                script.enabled = false;
                gameObject.layer = 2;

                anim.Play("Death");
        }
    }

    private void Ondie()
    {
        Destroy(gameObject);
    }

}

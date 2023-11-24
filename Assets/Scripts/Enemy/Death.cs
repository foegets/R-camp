using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;

    private Transform child;
    private 
    void Start()
    {
        child = transform.Find("DeathPoint");

        anim = GetComponent<Animator>();

        coll = child.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        var script = GetComponent<FSM>();
        if (coll.CompareTag("Player"))
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

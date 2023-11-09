using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour
{
    private float speed=1000;
    private Rigidbody2D rb;
    private Collider cd;
    private PlayerController pc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider>();
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag == "Player")
        {
            Debug.Log("TRAP!");
            rb.AddForce(pc.inputDirection * speed,ForceMode2D.Force);
        }
    }
}

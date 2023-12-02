using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trap : MonoBehaviour
{
    Vector2 startpos;

    private void Start()
    {
        startpos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        Respawn();
    }
    private void Respawn()
    {

        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{
    Vector2 starPos;
    private void Start()
    {
        starPos = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }
private void Die()
    {
        StartCoroutine(Respawn());
    }
   IEnumerator Respawn()  //¸´»î
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = starPos;
    }
}

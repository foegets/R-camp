using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamecontroller : MonoBehaviour
{
    Vector2 startpos;
    SpriteRenderer sp;
    Rigidbody2D rb;
    private void Start()
    {
        startpos = transform.position;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacie"))//�ж��Ƿ񴥷�
        {
            Die();
        }

    }
    private void Die()//����
    {
        StartCoroutine(Respawn());

    }
    IEnumerator Respawn()//����
    {
        sp.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.2f); //�ӳ�0.5s
        rb.bodyType = RigidbodyType2D.Dynamic;
        sp.enabled = true;
        transform.position = startpos;
    }
 
}

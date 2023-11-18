using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Collider2D boxCollider2D;//����������ײ��

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))//����ȥ
        {
            if (boxCollider2D == null) return;
            boxCollider2D.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))//����ȥ
        {
            if (boxCollider2D == null) return;
            boxCollider2D.enabled = true;
            boxCollider2D = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//��ײ���
    {
        if (collision.gameObject.tag != "Platform") return;
        boxCollider2D = collision.gameObject.GetComponent<Collider2D>();
    }
}

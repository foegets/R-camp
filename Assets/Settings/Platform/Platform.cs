using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Collider2D boxCollider2D;//变量储存碰撞体

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))//跳下去
        {
            if (boxCollider2D == null) return;
            boxCollider2D.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))//跳上去
        {
            if (boxCollider2D == null) return;
            boxCollider2D.enabled = true;
            boxCollider2D = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//碰撞检测
    {
        if (collision.gameObject.tag != "Platform") return;
        boxCollider2D = collision.gameObject.GetComponent<Collider2D>();
    }
}

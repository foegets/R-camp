using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float interval;
    public GameObject bullet1;
    private Transform tf;
    private Vector2 mousePos;
    private Vector2 direction;
    private Rigidbody2D rb;
    private float timer;
    
    void Start(){
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.velocity = (((mousePos - new Vector2(transform.position.x,transform.position.y)).normalized)*20);
    }

    public void OnTriggerEnter2D(Collider2D others){
        Destroy(gameObject,0.01f);
    }
}

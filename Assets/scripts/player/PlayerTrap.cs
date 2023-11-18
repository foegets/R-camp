using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerTrap : MonoBehaviour
{
    private float speed=1000;
    private Rigidbody2D rb;
    private Collider cd;
    private PlayerController pc;
    private Score score;

    public GameObject Prefab;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider>();
        pc = GetComponent<PlayerController>();
        score = GetComponent<Score>();
    }


    private void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag == "Player" && collision.gameObject.name == "Trap")
        {
            Debug.Log("TRAP!");
            rb.AddForce(transform.up * speed,ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Collections")
        {
            Debug.Log("Coin");
            Destroy(collision.gameObject);
            score.totalScore += score.coinScore;
        }

        if (gameObject.tag == "Player" && collision.gameObject.name == "brick")
        {
            Debug.Log("brick");
            Instantiate(Prefab);
        }
    }


}

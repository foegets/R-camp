using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class coin : MonoBehaviour
{
    private CircleCollider2D Coin;

    public int Score = 100;

    // Start is called before the first frame update
    void Start()
    {
        Coin = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Coin.enabled = false;
            gamecontroller.instance.totalScore += Score;
            gamecontroller.instance.UpdateTotalScore();

            Destroy(gameObject);
        }
    }


}




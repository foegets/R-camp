using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCoins : MonoBehaviour
{
    private Collider2D cd;
    public GameObject Coin;
    public GameObject Block;

    public void OnTriggerEnter2D(Collider2D Player){
        Instantiate(Coin, new Vector3(transform.position.x + 2, transform.position.y, 0), Quaternion.identity);
        for(int i =0;i<=3;i++){
        Instantiate(Block, new Vector3(transform.position.x + i + 1, transform.position.y - 1, 0), Quaternion.identity);
        }
        Destroy(gameObject,0.01f);
    }
}

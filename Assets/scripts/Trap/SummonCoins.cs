using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCoins : MonoBehaviour
{
    private Collider2D cd;
    public GameObject Coin;
    public GameObject Block;

    public void OnTriggerEnter2D(Collider2D Player){
        for(int i = 0;i<=2;i++){
        Instantiate(Coin, new Vector3(7.97f + i, 7f, 0), Quaternion.identity);
        }
        for(int i =0;i<=3;i++){
        Instantiate(Block, new Vector3(6.97f + i, 6.04f, 0), Quaternion.identity);
        }
        Destroy(gameObject,0.01f);
    }
}

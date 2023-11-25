using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    private Collider2D cd;
    public CoinsCounter cc;
    public GameObject CoinCounter;
    void Awake(){
    }
    public void OnTriggerEnter2D(Collider2D Player){
        CoinsCounter.currentCoinQuantity += 1;
        Destroy(gameObject,0.01f);
    }
}

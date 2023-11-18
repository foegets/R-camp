using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    private Collider2D cd;
    public CoinsCounter cc;
    public int coins;
    void Awake(){
        cc = GetComponent<CoinsCounter>();
        int coinQuantity = GetComponent<CoinsCounter>().coinQuantity;
    }
    public void OnTriggerEnter2D(Collider2D Player){
        cc.coinQuantity += 1;
        Destroy(gameObject,0.01f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinCount : MonoBehaviour
{
    public int CoinCount=0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GoldCoin")
        {
            CoinCount++;
        }
    }

}

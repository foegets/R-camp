using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    public int startCoin;
    public static int currentCoinQuantity;
    public Text coinQuanity;
    void Start(){
        currentCoinQuantity = startCoin;
    }
    void Update(){
        coinQuanity.text = currentCoinQuantity.ToString();
    }
}

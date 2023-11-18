using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    public int startCoin;
    public int coinQuantity;
    public Text t;
    void Start(){
        coinQuantity = startCoin;
    }
    void update(){
        t.text = coinQuantity.ToString();
    }
}

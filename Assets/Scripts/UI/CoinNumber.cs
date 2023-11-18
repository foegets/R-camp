using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinNumber : MonoBehaviour
{
    public GameObject player;
    private GoldCoinCount coinCount;
    public TextMeshProUGUI coinText;
    
    public int coinNumber; 
    private void Awake()
    {
        coinCount = player.GetComponent<GoldCoinCount>();
        
    }

    private void Update()
    {
        coinNumber = coinCount.CoinCount;
        coinText.text = coinNumber.ToString();
        
    }
}

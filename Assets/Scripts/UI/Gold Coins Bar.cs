using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoinsBar : MonoBehaviour
{
    //计数
    public int CoinCount;
    public GameObject scoreText;

    private void Awake()
    {
        CoinCount = 0;
    }

    public void OnPickCoin()
    {
        CoinCount ++;
        scoreText.GetComponent<Text>().text = CoinCount.ToString();
    }


}

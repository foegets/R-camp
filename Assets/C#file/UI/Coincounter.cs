using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coincounter : MonoBehaviour
{
    public int stCoin;
    public Text coinQuantity;
    public static int nowCoin;
    // Start is called before the first frame update
    void Start()
    {
        nowCoin = stCoin;

    }

    // Update is called once per frame
    void Update()
    {
        coinQuantity.text = nowCoin.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coinUi : MonoBehaviour
{
    // Start is called before the first frame update
    public Text coinNum;
    public int startCoinNum;//金币初始数目
    public int currentCoinNum;
    void Start()
    {
        currentCoinNum = startCoinNum;//更新金币数目
    }

    // Update is called once per frame
    void Update()
    {
        coinNum.text = currentCoinNum.ToString();//转化到文本上
        currentCoinNum = player.coinNum;
    }
}

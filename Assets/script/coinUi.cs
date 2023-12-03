using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coinUi : MonoBehaviour
{
    // Start is called before the first frame update
    public int startCoinNum;//金币初始数目
    public int currentCoinNum;
    private GameObject coinText;
    void Start()
    {
        currentCoinNum = startCoinNum;//更新金币数目
        coinText = GameObject.FindGameObjectWithTag("coinText");
    }

    // Update is called once per frame
    void Update()
    {
        coinText.GetComponent<Text>().text = currentCoinNum.ToString();
    }
}

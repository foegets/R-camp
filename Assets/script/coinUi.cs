using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coinUi : MonoBehaviour
{
    // Start is called before the first frame update
    public int startCoinNum;//��ҳ�ʼ��Ŀ
    public int currentCoinNum;
    private GameObject coinText;
    void Start()
    {
        currentCoinNum = startCoinNum;//���½����Ŀ
        coinText = GameObject.FindGameObjectWithTag("coinText");
    }

    // Update is called once per frame
    void Update()
    {
        coinText.GetComponent<Text>().text = currentCoinNum.ToString();
    }
}

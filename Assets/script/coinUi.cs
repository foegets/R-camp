using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coinUi : MonoBehaviour
{
    // Start is called before the first frame update
    public Text coinNum;
    public int startCoinNum;//��ҳ�ʼ��Ŀ
    public int currentCoinNum;
    void Start()
    {
        currentCoinNum = startCoinNum;//���½����Ŀ
    }

    // Update is called once per frame
    void Update()
    {
        coinNum.text = currentCoinNum.ToString();//ת�����ı���
        currentCoinNum = player.coinNum;
    }
}

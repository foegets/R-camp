using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : MonoBehaviour
{
    public TMP_Text UI;
    public int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)//���is trigger��ײ�����
    {
        if (other.tag=="Coin" )   //��������ǲ���coin//��������CompareTag("Coin")
        {   
            Score++;//����+1
            UI.text = "Score:" + Score;//�ı�ui
            Destroy(other.gameObject);//coin��ʧ

        }
    }
}

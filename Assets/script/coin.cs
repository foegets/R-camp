using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    private GameObject coinTextUI;

    // Start is called before the first frame update
    void Start()
    {
        coinTextUI = GameObject.FindGameObjectWithTag("coinUI");//�ҵ���Ϸ����
    }
    private void OnTriggerEnter2D(Collider2D other)//��ҵĴ������
    {
        if (other.gameObject.CompareTag("Player"))
        {
            coinTextUI.GetComponent<coinUi>().currentCoinNum++;
            this.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen2 : MonoBehaviour
{
    public bool isGen;
    public GameObject prefaCherry;

    private UIController uIController;//����ʵ��
    void Start()
    {
        uIController = UIController.instance;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("player")) //�ж���ײ��tag
        {
            if (isGen) 
            {
                Destroy(gameObject);
                LevelManeger.gensCollected++;

                GameObject cherry_0 = Instantiate(prefaCherry);// ����Ԥ����
                cherry_0.transform.position = new Vector2(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y+3);//�ı�Ԥ����λ��
            }
        }
    }



}

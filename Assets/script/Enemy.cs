using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  float health;//С������
    public   float maxHp;//С���������ֵ
    public  float damage;//С���˺�
    public GameObject injuryEffect;//��Ѫ��������Ч
    public  float damageTime;
    // Start is called before the first frame update
       void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void bloodEffect()
    {
       Instantiate(injuryEffect,this.transform.position, Quaternion.identity);//����������Ѫ����
    }
   
}

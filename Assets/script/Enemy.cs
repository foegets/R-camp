using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  float health;//小怪生命
    public   float maxHp;//小怪最大生命值
    public  float damage;//小怪伤害
    public GameObject injuryEffect;//流血的粒子特效
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
       Instantiate(injuryEffect,this.transform.position, Quaternion.identity);//激活受伤流血粒子
    }
   
}

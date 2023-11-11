using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//该代码用来实施攻击
public class Attack : MonoBehaviour
{
    public int damage;//定义伤害
    public float attackRange;//伤害范围
    public float attackRate;//伤害频率
    //以持续（区别于进入和出去）的方式
    private void OnTriggerStay2D(Collider2D other)
    {   
        //获取calculate组件，访问被攻击者，向其输入攻击数值,?表示先进行是否有攻击代码的判断
        other.GetComponent<Calculate>()?.TakeDamage(this);
    }

}

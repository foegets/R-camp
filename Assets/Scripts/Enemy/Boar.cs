using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    //重写父类中的move函数
    public override void Move()
    {   
        //固定格式，基于父类中的move函数的
        base.Move();
        //播放walk动画
        anim.SetBool("walk",true);
    }
}

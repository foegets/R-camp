using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy//有道是——类的继承！
{
    public override void Move()//重写/覆盖Move函数
    {
        base.Move();//保留原来的Move函数里的语句
        anim.SetBool("walk", true);//使Animator中的walk=true，使得boarWalk动画开始播放

    }
}

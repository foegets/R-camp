using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    public override void Move()
    {
        //base��ʾ���ڸ����Move
        base.Move();
        anim.SetBool("walk", true);
    }
}

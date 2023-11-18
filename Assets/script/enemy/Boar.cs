using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    public override void Move()
    {
        //base表示基于父类的Move
        base.Move();
        anim.SetBool("walk", true);
    }
}

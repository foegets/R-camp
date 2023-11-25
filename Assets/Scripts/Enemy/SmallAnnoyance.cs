using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAnnoyance : Enemy
{
    public override void Move()
    {
        base.Move();
        anim.SetBool("Move", true);
    }
}

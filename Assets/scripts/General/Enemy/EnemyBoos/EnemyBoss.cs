using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    
    protected override void Awake()
    {
        base.Awake();
        state_a = new FirstState();
        state_b = new SecondState();
        state_c = new FinalState();
        //deadState
        
    }

    protected override void Update()
    {
        base.Update();
    }
}

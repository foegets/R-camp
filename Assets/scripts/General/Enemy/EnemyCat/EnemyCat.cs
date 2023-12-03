using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : Enemy
{

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        state_a = new CatPatrolState();
        state_b = new CatChaseState();
        deadState = new CaiDeadState();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

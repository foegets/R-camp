using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : Enemy
{

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        patrolState = new CatPatrolState();
        chaseState = new CatChaseState();
        deadState = new CaiDeadState();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiDeadState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        Debug.Log("Dead Onenter");
        currentEnemy = enemy;
        currentEnemy.rigid.velocity = Vector3.zero;
        currentEnemy.GetComponent<Attack>().enabled = false;
        currentEnemy.character.enabled = false;
       
    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        
    }
}

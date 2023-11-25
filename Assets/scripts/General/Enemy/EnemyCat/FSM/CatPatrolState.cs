using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }

    public override void LogicUpdate()
    {
        //todo ToChase
        if (currentEnemy.isInLeftStandbyCheckPoint)
        {
            currentEnemy.transform.localScale = new Vector3(1,1,1);
        }
        if (currentEnemy.isInRightStandbyCheckPoint)
        {
            currentEnemy.transform.localScale = new Vector3(-1,1,1);
        }
        if(currentEnemy.isPlayerinRange == true)
        {
            currentEnemy.currentState = currentEnemy.chaseState;
            currentEnemy.currentState.OnEnter(currentEnemy);
        }
    }

    public override void PhysicsUpdate()
    {
        Move();
    }

    public override void OnExit()
    {

    }

    protected virtual void Move()
    {
        currentEnemy.rigid.velocity = new Vector3(currentEnemy.faceDir * currentEnemy.walkSpeed, 0, 0);
        currentEnemy.isWalk = true;
    }
}

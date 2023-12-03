using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPortalState : baseState
{
    protected Enemy currentEnemy;

    public override void LogicUpdate()
    {
        if(currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
            //Debug.Log("hi");
        }

        if (!currentEnemy.physicCheck.isGround||(currentEnemy.physicCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.animator.SetBool("isWalk", false);
        }
        else
        {
            currentEnemy.animator.SetBool("isWalk", true);
        }
    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.norlmalSpeed;
    }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("isWalk", false);
        Debug.Log("exit");
    }

    public override void PhysicsUpdate()
    {
        // new System.NotImplementedException();
    }
}

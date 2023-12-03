using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatChaseState : BaseState
{
    private float playerDir;
    private float standbyPointDir;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.isRun = true;
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.isPlayerinRange)
        {
            playerDir = currentEnemy.checkPlayer.transform.position.x - currentEnemy.transform.position.x;
            if (playerDir >= 0)
                currentEnemy.transform.localScale = new Vector3(1, 1, 1);
            if (playerDir < 0)
                currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        }

        if(currentEnemy.isPlayerinRange == false)
        {
            if(currentEnemy.isStandbyPoint == false)
            {
                standbyPointDir = currentEnemy.standbyPoint.x - currentEnemy.transform.position.x;
                if (standbyPointDir >= 0)
                    currentEnemy.transform.localScale = new Vector3(1, 1, 1);
                if (standbyPointDir < 0)
                    currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                currentEnemy.isRun = false;
                currentEnemy.isWalk = true;
                currentEnemy.currentState = currentEnemy.state_a;
                currentEnemy.currentState.OnEnter(currentEnemy);
            }
            
        }
    }

    public override void PhysicsUpdate()
    {
        Run();
    }

    public override void OnExit()
    {
    //    currentEnemy.isRun = false;
    //    currentEnemy.currentState = currentEnemy.patrolState;
    //    currentEnemy.currentState.OnEnter(currentEnemy);
    }

    protected virtual void Run()
    {
        currentEnemy.rigid.velocity = new Vector3(currentEnemy.faceDir * currentEnemy.runSpeed, 0, 0);
        currentEnemy.isWalk = true;
    }

}

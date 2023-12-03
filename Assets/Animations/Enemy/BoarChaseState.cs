using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class BoarChaseState : baseState
{
    protected Enemy currentEnemy;
    // Start is called before the first frame update

    public override void OnEnter(Enemy enemy)
    {
        Debug.Log("enter");
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetBool("isRun", true);

    }

    public override void LogicUpdate()
    {
        if(currentEnemy.lostTimeCounter<=0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        if (!currentEnemy.physicCheck.isGround || (currentEnemy.physicCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }

    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }

    public override void PhysicsUpdate()
    {
        currentEnemy.animator.SetBool("isRun", false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class IdleState : IState
{
    private FSMP manager;
    private Parameter parameter;
    private float timer;
    private int chasePosition;

    public IdleState(FSMP manager)
    {
        this.manager = manager; 
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        Debug.Log("Idle!");
        chasePosition = 1 - ((int)manager.transform.localScale.x + 1) / 2;
        parameter.animator.Play("Idle");
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if(timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
        }

        if ((parameter.target.transform.position.x) * (manager.transform.localScale.x) > (manager.transform.position.x) * (manager.transform.localScale.x)    //这里通过判断目标的x坐标值是否在 狼人 的和ChasePosition之间，                
            && (parameter.target.transform.position.x) * (manager.transform.localScale.x) < (parameter.chasePoints[chasePosition].position.x) * (manager.transform.localScale.x))
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit() 
    {
        timer = 0;
    }
}

public class PatrolState : IState
{
    private FSMP manager;
    private Parameter parameter;

    private int patrolPosition;
    private int chasePosition;

    public PatrolState(FSMP manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        Debug.Log("Patrol!");
        parameter.animator.Play("Walk");
       
    }
    public void OnUpdate()
    {
        manager.FlipTo(parameter.patrolPoints[patrolPosition]);                                    
        chasePosition = 1 - (int)(manager.transform.localScale.x + 1) / 2;            //这里设置了chasePosition的值，使其始终在狼人前方

        manager.transform.position = new Vector2(Vector2.MoveTowards(manager.transform.position, 
            parameter.patrolPoints[patrolPosition].position,parameter.moveSpeed * Time.deltaTime).x, manager.transform.position.y);
       
        if ((parameter.target.transform.position.x)*(manager.transform.localScale.x)>(manager.transform.position.x)*(manager.transform.localScale.x)    //这里通过判断目标的x坐标值是否在 狼人 的和ChasePosition之间，                
            && (parameter.target.transform.position.x)*(manager.transform.localScale.x) < (parameter.chasePoints[chasePosition].position.x) * (manager.transform.localScale.x))           
        {
            manager.TransitionState(StateType.Chase);
        }

        if (Math.Abs(manager.transform.position.x-parameter.patrolPoints[patrolPosition].position.x) < .1f)
        {
            
            manager.TransitionState(StateType.Idle); 
        }
    }

    public void OnExit()
    {
        
        patrolPosition++;

        if(patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }
}

public class ChaseState : IState
{
    private FSMP manager;
    private Parameter parameter;

    public int chasePosition;

    public ChaseState(FSMP manager)
    {
        Debug.Log("Chase!");
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {   
        parameter.animator.Play("Run");
    }
    public void OnUpdate()
    {
        if (manager.transform.position.x > parameter.target.transform.position.x)       //这个if语句使得狼人在追击目标时始终朝向目标，并使chasePosition一直在狼人前方
        {
            manager.transform.localScale = new Vector3(-1,1, 1);
            chasePosition = 1;
        }
        else if (manager.transform.position.x < parameter.target.transform.position.x)
        {
            manager.transform.localScale = new Vector3(1,1,1);
            chasePosition = 0;
        }

        manager.transform.position = new Vector2(Vector2.MoveTowards(manager.transform.position,                       
           parameter.target.transform.position, parameter.chaseSpeed * Time.deltaTime).x,manager.transform.position.y);

        if ((parameter.target.transform.position.x - manager.transform.position.x)*manager.transform.localScale.x < parameter.rushRange)
        {
           manager.TransitionState(StateType.Rush);
        }

        if (Math.Abs(manager.transform.position.x-parameter.chasePoints[chasePosition].position.x) < .1f ||                            //如果狼人靠近chasePoint，则会停下                  
             (parameter.target.transform.position.x - parameter.chasePoints[chasePosition].position.x)*manager.transform.localScale.x>0.1f)            //如果玩家离开了追击范围，则狼人停下    
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        chasePosition++;
        if (chasePosition >= parameter.patrolPoints.Length)
        {
            chasePosition = 0;
        }
    }
}
public class RushState : IState
{
    private FSMP manager;
    private Parameter parameter;
    private float timer;
   
    
    public RushState(FSMP manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        Debug.Log("RushEnter!");
         //parameter.rushRange / (parameter.rushSpeed * Time.fixedDeltaTime)*4;       
        parameter.animator.Play("Rush");

    }
    public void OnUpdate()
    {
        Debug.Log("RushUpdate");
        manager.transform.position = new Vector2(manager.transform.position.x
            + Time.fixedDeltaTime * parameter.rushSpeed * manager.transform.localScale.x
            , manager.transform.position.y);                                                                             //有暂时无法解决的bug，进入RushState后会直接退出
        timer++;

    }
    public void OnExit()
    {
        Debug.Log("ExitRush");
        timer = 0;
    }
}

public class AttackState : IState
{
    private FSMP manager;
    private Parameter parameter;
    private float timer;
    private int chasePosition;
    public AttackState(FSMP manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class HurtState : IState
{
    private FSMP manager;
    private Parameter parameter;
    private float timer;
    private int chasePosition;
    public HurtState(FSMP manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class DeadState : IState
{
    private FSMP manager;
    private Parameter parameter;
    private float timer;
    private int chasePosition;
    public DeadState(FSMP manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
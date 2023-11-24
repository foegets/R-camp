using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Idle");
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if(parameter.player != null && parameter.player.position.x >= parameter.chasePoints[0].position.x &&     //玩家进入sight
           parameter.player.position.x <= parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }

        if(timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);                      //等待完进入巡逻状态
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}

public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;

    private int patrolPosition;
    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Move");
    }

    public void OnUpdate()
    {
        manager.FilpTo(parameter.patrolPoints[patrolPosition]);

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.patrolPoints[patrolPosition].position,parameter.moveSpeed * Time.deltaTime);            //向巡逻点移动

        if (parameter.player != null && parameter.player.position.x >= parameter.chasePoints[0].position.x &&    //玩家进入sight
           parameter.player.position.x <= parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }

        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < .1f )  //到巡逻点后进入等待
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        patrolPosition++;

        if(patrolPosition >=parameter.patrolPoints.Length)
        {
            patrolPosition = 0;                                              //使在两个巡逻点之间循环移动
        }
    }
}

public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Chase");
    }

    public void OnUpdate()
    {
        manager.FilpTo(parameter.player);
        if(parameter.player)
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.player.position,parameter.chaseSpeed * Time.deltaTime);

        if(parameter.player == null ||
            manager.transform.position.x < parameter.chasePoints[0].position.x || 
            manager.transform.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);
        }

        if(Physics2D.OverlapCircle(parameter.attackPoint.position,parameter.attackArea,parameter.playerLayer))
        {
            manager.TransitionState(StateType.Attack);
        }
           

        
    }

    public void OnExit()
    {

    }
}

public class ReactState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;
    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Idle");
    }

    public void OnUpdate()
    {
        info = parameter.anim.GetCurrentAnimatorStateInfo(0);

        if(info.normalizedTime >= 0.1f)
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {

    }
}

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Attack");
    }

    public void OnUpdate()
    {
        info = parameter.anim.GetCurrentAnimatorStateInfo(0);

        if(info.normalizedTime >= 0.95f)
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {

    }
}
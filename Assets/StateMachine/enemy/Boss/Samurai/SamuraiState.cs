using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai_IdleState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;
    private int timer;
    

    public Samurai_IdleState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Samurai_Idle");
        timer = 0;
    }

    public void OnExit()
    {
        timer = 0;
    }

    public void OnUpdate()
    {
        if (manager.character.isDead)
        {
            manager.TransitionState(StateType_Samurai.Dead);
            return;
        }

        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType_Samurai.Hurt);
            return;
        }

       
            timer++;
            if(timer>parameter.idleTime)
            {

                manager.TransitionState(StateType_Samurai.Walk);
            }
        

        
    }
}

public class Samurai_WalkState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;
    private System.Random random;
    private int timer;
    private int willDash;
    public Samurai_WalkState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        random = new System.Random();
        timer = 0;
        parameter.animator.Play("Samurai_Walk");
        willDash = random.Next(0, 3);
    }

    public void OnExit()
    {
       
    }

    public void OnUpdate()
    {
        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType_Samurai.Hurt);
            return;
        }

        timer++;

        manager.FlipTo(parameter.target.transform);
       
        manager.transform.position = new Vector2(Vector2.MoveTowards(manager.transform.position,
           parameter.target.transform.position, parameter.WalkSpeed * Time.deltaTime).x, manager.transform.position.y);

        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType_Samurai.Hurt);
            return;
        }

        if (Math.Abs(parameter.target.transform.position.x - manager.transform.position.x) > parameter.walkDistance)
        {
            manager.TransitionState(StateType_Samurai.Run);
            return;
        }

        
        if(Math.Abs(parameter.target.transform.position.x - manager.transform.position.x) < parameter.attackDistance)
        {
            manager.TransitionState(StateType_Samurai.Attack);
            return;
        }

        if (Math.Abs(parameter.target.transform.position.x - manager.transform.position.x) < parameter.rushDistance&&(willDash ==1))
        {
            manager.TransitionState(StateType_Samurai.Rush);
            return;
        }
    }
}

public class Samurai_RunState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;

    public Samurai_RunState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {

        parameter.animator.Play("Samurai_Run");
        
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType_Samurai.Hurt);
            return;
        }

        manager.FlipTo(parameter.target.transform);
        manager.transform.position = new Vector2(Vector2.MoveTowards(manager.transform.position,
           parameter.target.transform.position, parameter.RunSpeed * Time.deltaTime).x, manager.transform.position.y);

        if (Math.Abs(parameter.target.transform.position.x - manager.transform.position.x) < parameter.walkDistance)
        {
            
            manager.TransitionState(StateType_Samurai.Walk);
            return;
        }
    }
}

public class Samurai_HurtState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;
    public Vector2 dir;

    public Samurai_HurtState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Samurai_Hurt");
        manager.character.TriggerInvulnerable();
        dir = Vector2.zero;
    }

    public void OnExit()
    {
       
    }

    public void OnUpdate()
    {

        if (manager.character.isDead)
        {
            manager.TransitionState(StateType_Samurai.Dead);
            return;
        }

        if ( !manager.character.isInvulnerable )
        {
            manager.TransitionState(StateType_Samurai.Idle);
            return;
        }

        manager.rb.velocity = Vector2.zero;
        if (parameter.target.transform.position.x >= manager.transform.position.x)
        {
            dir.x = -1;
        }
        else if (parameter.target.transform.position.x < manager.transform.position.x)
        {
            dir.x = 1;

        }

        manager.rb.AddForce(dir * parameter.hurtForce, ForceMode2D.Impulse);
    }
}

public class Samurai_DeadState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;

    public Samurai_DeadState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Samurai_Dead");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        
    }
}

public class Samurai_DefendState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;

    public Samurai_DefendState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}

public class Samurai_RushState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;


    public Samurai_RushState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Samurai_Attack1");
        manager.playFinished = false;
    }

    public void OnExit()
    {
        manager.playFinished = false;
    }

    public void OnUpdate()
    {

        if (manager.playFinished)
        {
            manager.TransitionState(StateType_Samurai.Idle);
        }
    }
}

public class Samurai_AttackState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;

    public Samurai_AttackState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        manager.playFinished = false;
        parameter.animator.Play("Samurai_Attack2");
    }

    public void OnExit()
    {
       
    }

    public void OnUpdate()
    {
        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType_Samurai.Hurt);
            return;
        }

        if (manager.playFinished)
        {
            parameter.animator.Play("Samurai_Attack3");
            if (manager.playFinished2)
            {
                manager.TransitionState(StateType_Samurai.Idle);
            }
        }
    }
}

public class Samurai_EnterState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;

    public Samurai_EnterState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Samurai_Enter");
    }

    public void OnExit()
    {
       
    }

    public void OnUpdate()
    {
        
    }
}

public class Samurai_WaitState : IState
{
    private Samurai_FSM manager;
    private Samurai_Parameter parameter;

    public Samurai_WaitState(Samurai_FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.Samurai_Parameter;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
       
    }
}
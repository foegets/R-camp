using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEditor.Experimental.GraphView;
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

    public IdleState()
    {
    }

    public void OnEnter()
    {

        chasePosition = 1 - ((int)manager.transform.localScale.x + 1) / 2;
        parameter.animator.Play("Idle");
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType.Hurt);
            return;
        }

        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
        }

        if ((parameter.target.transform.position.x) * (manager.transform.localScale.x) > (manager.transform.position.x) * (manager.transform.localScale.x)    //这里通过判断目标的x坐标值是否在 狼人 的和ChasePosition之间，                
            && (parameter.target.transform.position.x) * (manager.transform.localScale.x) < (parameter.chasePoints[chasePosition].position.x) * (manager.transform.localScale.x))
        {
            manager.TransitionState(StateType.Chase);
        }

        if(manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType.Hurt);
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
        
        parameter.animator.Play("Walk");
       
    }
    public void OnUpdate()
    {
        manager.FlipTo(parameter.patrolPoints[patrolPosition]);                                    
        chasePosition = 1 - (int)(manager.transform.localScale.x + 1) / 2;            //这里设置了chasePosition的值，使其始终在自身前方

        manager.transform.position = new Vector2(Vector2.MoveTowards(manager.transform.position, 
            parameter.patrolPoints[patrolPosition].position,parameter.moveSpeed * Time.deltaTime).x, manager.transform.position.y);

        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType.Hurt);
            return;
        }

        if ((parameter.target.transform.position.x)*(manager.transform.localScale.x)>(manager.transform.position.x)*(manager.transform.localScale.x)    //这里通过判断目标的x坐标值是否在 自身 的和ChasePosition之间，                
            && (parameter.target.transform.position.x)*(manager.transform.localScale.x) < (parameter.chasePoints[chasePosition].position.x) * (manager.transform.localScale.x) 
            &&Math.Abs((parameter.target.transform.position.y-manager.transform.position.y))<1)           
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

        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {   
        parameter.animator.Play("Run");
    }
    public void OnUpdate()
    {
        if (manager.transform.position.x > parameter.target.transform.position.x)       //这个if语句使得自身在追击目标时始终朝向目标，并使chasePosition一直在狼人前方
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


        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType.Hurt);
            return;
        }

        if ((parameter.target.transform.position.x - manager.transform.position.x)*manager.transform.localScale.x < parameter.rushRange)
        {
           manager.TransitionState(StateType.Rush);
            return;
        }
                                                                                  
        if (Math.Abs(manager.transform.position.x-parameter.chasePoints[chasePosition].position.x) < .1f ||                            //如果自身靠近chasePoint，则会停下                  
             (parameter.target.transform.position.x - parameter.chasePoints[chasePosition].position.x)*manager.transform.localScale.x>0.1f)            //如果玩家离开了追击范围，则狼人停下    
        {
             manager.TransitionState(StateType.Idle);                                                                              //问题：为什么Idle的条件和Rush冲突？
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
    private float rushTime;
    private Rigidbody2D rb;
   
    
    public RushState(FSMP manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
       manager.character.isAttacking = true;    
        rushTime = parameter.rushRange / (parameter.rushSpeed * Time.fixedDeltaTime)*3;       
        parameter.animator.Play("Rush");


    }
    public void OnUpdate()
    {

       manager.transform.position = new Vector2(manager.transform.position.x
           + Time.fixedDeltaTime * parameter.rushSpeed * manager.transform.localScale.x
           , manager.transform.position.y);

        if (manager.character.isUnderAttack)
        {
            manager.TransitionState(StateType.Hurt);
            return;
        }

       
        timer++;
        if(timer > rushTime) 
        {
            manager.TransitionState(StateType.Idle);
        }

    }
    public void OnExit()
    {
        manager.character.isAttacking = false;
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
    public Vector2 dir;
    public HurtState(FSMP manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
        
    }
    public void OnEnter()
    {
        parameter.animator.Play("Hurt");
        manager.character.TriggerInvulnerable();
        dir = Vector2.zero;
    }
    public void OnUpdate()
    {
        if (!manager.character.isInvulnerable)
        {
            manager.TransitionState(StateType.Idle);
        }
        if(manager.character.health <= 0)
        {
            Debug.Log("dead");
            manager.TransitionState(StateType.Dead);
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
        parameter.animator.Play("Dead");
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
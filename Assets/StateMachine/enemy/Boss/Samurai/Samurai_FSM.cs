using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Samurai_Parameter
{
    public float WalkSpeed;
    public float RunSpeed;
    public float rushSpeed;
    public float idleTime;
    
    public float walkDistance;                       //当玩家与武士的位移差大于walkDistance时，将会开始跑步
    public float rushDistance;
    public float attackDistance;
    public float rushRange;                          //冲刺的距离
    
    public Animator animator;
    public GameObject target;

    public bool isActivated;

    public float hurtForce;
}

public enum StateType_Samurai
{
    Idle,Walk,Run,Hurt,Dead,Defend,Enter,Rush,Attack,Wait
}


public class Samurai_FSM : MonoBehaviour
{
    public Samurai_Parameter Samurai_Parameter;
    public Character character;
    public Rigidbody2D rb;
    private IState currentState;
    private Dictionary<StateType_Samurai, IState> states = new Dictionary<StateType_Samurai, IState>();
    public bool isFaceLeft;
    public bool playFinished;
    public bool playFinished2;

    private void Start()
    {
        states.Add(StateType_Samurai.Idle, new Samurai_IdleState(this));
        states.Add(StateType_Samurai.Walk, new Samurai_WalkState(this));
        states.Add(StateType_Samurai.Run, new Samurai_RunState(this));
        states.Add(StateType_Samurai.Hurt, new Samurai_HurtState(this));
        states.Add(StateType_Samurai.Dead, new Samurai_DeadState(this));
        states.Add(StateType_Samurai.Defend, new Samurai_DefendState(this));
        states.Add(StateType_Samurai.Enter, new Samurai_EnterState(this));
        states.Add(StateType_Samurai.Rush, new Samurai_RushState(this));
        states.Add(StateType_Samurai.Attack, new Samurai_AttackState(this));
        states.Add(StateType_Samurai.Wait, new Samurai_WaitState(this));


        TransitionState(StateType_Samurai.Wait);
        Samurai_Parameter.target = GameObject.Find("player");
    }


    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType_Samurai type)
    {
        if (currentState! != null)
        {
            currentState.OnExit();
        }
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (transform.position.x < target.transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Attack1Dash()
    {
        transform.position = new Vector3(transform.position.x+Samurai_Parameter.rushRange*transform.localScale.x,transform.position.y,transform.position.z);
    }

   
}

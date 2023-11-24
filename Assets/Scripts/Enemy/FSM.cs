using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum StateType
{
    Idle,Patrol,Chase,React,Attack
}

[Serializable]
public class Parameter
{
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;
    public Transform[] patrolPoints;
    public Transform[] chasePoints;
    public Animator anim;
    public Transform player;
    public LayerMask playerLayer;
    public Transform attackPoint;
    public float attackArea;
}

public class FSM : MonoBehaviour
{
    public Parameter parameter;
    private IState currentState;

    private Dictionary<StateType,IState> states = new Dictionary<StateType,IState>();

    void Start()
    {
        parameter.anim = GetComponent<Animator>();

        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));

        TransitionState(StateType.Idle);
    }
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        if (currentState != null)
            currentState.OnExit();

        currentState = states[type];
        currentState.OnEnter();
    }

    public void FilpTo(Transform player)
    {
        if (player != null)
        {
            if(transform.position.x > player.transform.position.x)
            {
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (transform.position.x < player.transform.position.x)
            { 
                transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            parameter.player = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.player = null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);
    }
        
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static UnityEditor.VersionControl.Asset;

[Serializable]
public class Parameter 
{
    public int health;
    public float moveSpeed;
    public float chaseSpeed;
    public float rushSpeed;
    public float idleTime;
    public GameObject target;
    public float rushRange;
    public Transform[] patrolPoints;
    public Transform[] chasePoints;
    public Animator animator;
    
}

public enum StateType
{
    Idle, Patrol, Chase, Hurt, Attack,Rush,Dead
}



public class FSMP : MonoBehaviour  
{
    public Parameter parameter;
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    public bool isFaceLeft;
    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Hurt, new HurtState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Rush, new RushState(this));
        states.Add(StateType.Dead, new DeadState(this));

        TransitionState(StateType.Patrol);
        
        parameter.animator = GetComponent<Animator>();
        parameter.target = GameObject.Find("player");
    }


    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        if(currentState! != null)
        {
            currentState.OnExit();
        }
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo(Transform target)
    {
         if(target != null)
        {
            if (transform.position.x > target.transform.position.x)                
                transform.localScale = new Vector3(-1, 1, 1);
            else if (transform.position.x < target.transform.position.x)                
                transform.localScale = new Vector3(1, 1, 1); 
         }
    }
}

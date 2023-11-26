using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    Move,
    Attack,
    Patrol,
    Battle,
    OutBattle,
    Defend,
}

public interface IState
{
    // 状态进入时
    void OnEnter();
    // 状态退出时
    void OnExit();
    // 状态进行时
    void OnUpdata();
}

public class FSM
{
    
}

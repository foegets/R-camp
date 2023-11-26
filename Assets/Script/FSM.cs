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
    // ״̬����ʱ
    void OnEnter();
    // ״̬�˳�ʱ
    void OnExit();
    // ״̬����ʱ
    void OnUpdata();
}

public class FSM
{
    
}

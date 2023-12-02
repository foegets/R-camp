using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{  
    Dead,
    Idle,
    Patrol,
    Catch,
    Attack,
    Defend,
    Return,
}

public interface IState
{
    // 状态进入时
    void OnEnter();
    // 状态退出时
    void OnExit();
    // 状态进行时
    void OnUpdata();
    // 以下两个可有可无，上面三个必须存在
    //void OnCheck();
    //void OnFixUpdata();
}
// 创建用于共享数据的"黑板"(一种设计模式)
[Serializable]
public class BlackBoard
{
    
}

public class FSM
{
    // 当前状态
    public IState CurState;
    // 关联状态和与状态相应的响应逻辑的接口
    public Dictionary<StateType, IState> StateDic;
    // 创建"黑板"
    public BlackBoard black_board;
    // 初始化
    public FSM(BlackBoard black_board)
    {
        StateDic = new Dictionary<StateType, IState>();
        this.black_board = black_board;
    }
    // 添加状态
    public void AddState(StateType state_type, IState state)
    {
        if (StateDic.ContainsKey(state_type))
        {
            Debug.Log("已添加过当前状态");
            return;
        }
        StateDic.Add(state_type, state);
    }
    // 切换状态
    public void SwitchState(StateType state_type)
    {
        if (!StateDic.ContainsKey(state_type))
        {
            Debug.Log("没有有效状态可供切换");
            return;
        }
        // 退出当前状态
        if (CurState != null)
        {
            CurState.OnExit();
        }
        // 切换到目标状态
        CurState = StateDic[state_type];
        CurState.OnEnter();
    }
    // 当前状态要执行的逻辑
    public void CurStateUpdate()
    {
        CurState.OnUpdata();
    }
    
    //public void CurStateCheck()
    //{
    //    CurState.OnCheck();
    //}
    //public void CurStateFixUpdate()
    //{
    //    CurState.OnFixUpdate();
    //}
}

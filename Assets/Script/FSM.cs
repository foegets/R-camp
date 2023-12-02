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
    // ״̬����ʱ
    void OnEnter();
    // ״̬�˳�ʱ
    void OnExit();
    // ״̬����ʱ
    void OnUpdata();
    // �����������п��ޣ����������������
    //void OnCheck();
    //void OnFixUpdata();
}
// �������ڹ������ݵ�"�ڰ�"(һ�����ģʽ)
[Serializable]
public class BlackBoard
{
    
}

public class FSM
{
    // ��ǰ״̬
    public IState CurState;
    // ����״̬����״̬��Ӧ����Ӧ�߼��Ľӿ�
    public Dictionary<StateType, IState> StateDic;
    // ����"�ڰ�"
    public BlackBoard black_board;
    // ��ʼ��
    public FSM(BlackBoard black_board)
    {
        StateDic = new Dictionary<StateType, IState>();
        this.black_board = black_board;
    }
    // ���״̬
    public void AddState(StateType state_type, IState state)
    {
        if (StateDic.ContainsKey(state_type))
        {
            Debug.Log("����ӹ���ǰ״̬");
            return;
        }
        StateDic.Add(state_type, state);
    }
    // �л�״̬
    public void SwitchState(StateType state_type)
    {
        if (!StateDic.ContainsKey(state_type))
        {
            Debug.Log("û����Ч״̬�ɹ��л�");
            return;
        }
        // �˳���ǰ״̬
        if (CurState != null)
        {
            CurState.OnExit();
        }
        // �л���Ŀ��״̬
        CurState = StateDic[state_type];
        CurState.OnEnter();
    }
    // ��ǰ״̬Ҫִ�е��߼�
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

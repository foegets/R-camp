using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[Serializable]
public class PatrolEnemyBlackBoard : BlackBoard
{
    public UnityEngine.Transform player;
    #region �����ز���
    [Header("ǽ�ڼ�����")]
    // ����ǽ�ڼ����߼�����
    public float RayToMonitorWall_Distance = 10f;
    // ��ȡ���ǽ�ڲ㼶����
    public LayerMask WallLayer;

    [Header("��Ҽ�����")]
    // �������μ������ĽǶ�
    public float MonitorDeg = 120f;
    // �������μ������İ뾶
    public float MonitorRad = 15f;
    // ��ȡ��ҵĲ㼶����
    public LayerMask PlayerLayer;
    // ������߼�����
    protected RaycastHit hitObject;
    // �����ʼ���Y��ƫ����
    public float Yoffset = 0.5f;
    #endregion

    public float IdleTime;
    public StateType nextState;
    public float PatrolTime;
    // �ƶ��ٶ�
    public float movespeed = 8f;
    // ��ǰλ��
    public UnityEngine.Transform curPos;
    #region ���߼�����
    // ������
    public void SearchPlayer()
    {
        float rightangle = curPos.rotation.eulerAngles.y + MonitorDeg / 2;
        float leftangle = curPos.rotation.eulerAngles.y - MonitorDeg / 2;
        for (float i = leftangle; i <= rightangle; i += 2)
        {
            Vector3 MonitorDir = new Vector3(Mathf.Sin(i * Mathf.Deg2Rad), curPos.position.y + Yoffset, Mathf.Cos(i * Mathf.Deg2Rad));
            if (Physics.Raycast(curPos.position + new Vector3(0, Yoffset, 0), MonitorDir, out hitObject, MonitorRad, PlayerLayer))
            {
                if (hitObject.collider.CompareTag("Player"))
                {
                    // ����״̬
                    // ���õ�����Ϊ
                    curPos.LookAt(player.position);
                }
            }
        }
        
    }
    // ���ǽ��
    protected void DetectWall()
    {
        if (Physics.Raycast(curPos.position + new Vector3(0, Yoffset, 0), curPos.forward + new Vector3(0, Yoffset, 0), RayToMonitorWall_Distance, WallLayer))
        {
            curPos.rotation = Quaternion.Euler(curPos.eulerAngles.x, - curPos.eulerAngles.y, curPos.eulerAngles.z);
        }
    }
    #endregion
}



public class PatrolTypeEnemyAI : MonoBehaviour
{
    // ����״̬��
    private FSM PTE_fsm;
    // �����ڰ�
    public PatrolEnemyBlackBoard black_board;
    void Start()
    {
        PTE_fsm = new FSM(black_board);
        PTE_fsm.AddState(StateType.Dead, new isDead(PTE_fsm));
        PTE_fsm.AddState(StateType.Idle, new EnemyIdle(PTE_fsm));
        PTE_fsm.AddState(StateType.Patrol, new EnemyPatrol(PTE_fsm));
        PTE_fsm.AddState(StateType.Catch, new EnemyCatch(PTE_fsm));
        PTE_fsm.AddState(StateType.Attack, new EnemyAttack(PTE_fsm));
        PTE_fsm.AddState(StateType.Defend, new EnemyDefend(PTE_fsm));
        PTE_fsm.AddState(StateType.Return, new EnemyReturn(PTE_fsm));
        PTE_fsm.SwitchState(StateType.Idle);
    }

    
    void Update()
    {
        transform.position = black_board.curPos.position;
        transform.rotation = black_board.curPos.rotation;
        // ִ�е�ǰ
        PTE_fsm.CurStateUpdate();
    }
}

public class isDead : IState
{
    private float Timer;

    private FSM fsm;

    private PatrolEnemyBlackBoard black_board;

    public isDead(FSM fsm)
    {
        this.fsm = fsm;
        this.black_board = fsm.black_board as PatrolEnemyBlackBoard;
    }

    public void OnEnter()
    {
        Timer = 0;
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    public void OnUpdata()
    {
        throw new NotImplementedException();
    }
}

public class EnemyIdle : IState
{
    private float Timer;

    private FSM fsm;

    private PatrolEnemyBlackBoard black_board;

    public EnemyIdle(FSM fsm)
    {
        this.fsm= fsm;
        this.black_board = fsm.black_board as PatrolEnemyBlackBoard;
    }

    public void OnEnter()
    {
        Timer = 0;
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    public void OnUpdata()
    {
        Timer += Time.deltaTime;
        if (Timer >= black_board.IdleTime)
        {
            fsm.SwitchState(black_board.nextState);
        }
    }
}

public class EnemyPatrol : IState
{
    private float Timer;

    private FSM fsm;

    private PatrolEnemyBlackBoard black_board;

    public EnemyPatrol(FSM fsm)
    {
        black_board.curPos.position += black_board.curPos.forward * black_board.movespeed * Time.deltaTime;
        this.fsm = fsm;
        this.black_board = fsm.black_board as PatrolEnemyBlackBoard;
    }

    public void OnEnter()
    {
        Timer = 0;
        
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    public void OnUpdata()
    {
        Timer += Time.deltaTime;

        if (Timer >= black_board.PatrolTime)
        {
            black_board.IdleTime = 2f;
            fsm.SwitchState(StateType.Patrol);
        }
    }

}

public class EnemyCatch : IState
{
    private float Timer;

    private FSM fsm;

    private PatrolEnemyBlackBoard black_board;

    public EnemyCatch(FSM fsm)
    {
        this.fsm = fsm;
        this.black_board = fsm.black_board as PatrolEnemyBlackBoard;
    }

    public void OnEnter()
    {
        Timer = 0;
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    public void OnUpdata()
    {
        throw new NotImplementedException();
    }
}

public class EnemyAttack : IState
{
    private float Timer;

    private FSM fsm;

    private PatrolEnemyBlackBoard black_board;

    public EnemyAttack(FSM fsm)
    {
        this.fsm = fsm;
        this.black_board = fsm.black_board as PatrolEnemyBlackBoard;
    }

    public void OnEnter()
    {
        Timer = 0;
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    public void OnUpdata()
    {
        throw new NotImplementedException();
    }
}

public class EnemyDefend : IState
{
    private float Timer;

    private FSM fsm;

    private PatrolEnemyBlackBoard black_board;

    public EnemyDefend(FSM fsm)
    {
        this.fsm = fsm;
        this.black_board = fsm.black_board as PatrolEnemyBlackBoard;
    }

    public void OnEnter()
    {
        Timer = 0;
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    public void OnUpdata()
    {
        throw new NotImplementedException();
    }

}

public class EnemyReturn : IState
{
    private float Timer;

    private FSM fsm;

    private PatrolEnemyBlackBoard black_board;

    public EnemyReturn(FSM fsm)
    {
        this.fsm = fsm;
        this.black_board = fsm.black_board as PatrolEnemyBlackBoard;
    }

    public void OnEnter()
    {
        throw new NotImplementedException();
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    public void OnUpdata()
    {
        throw new NotImplementedException();
    }
}
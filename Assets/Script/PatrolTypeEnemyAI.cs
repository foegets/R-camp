using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[Serializable]
public class PatrolEnemyBlackBoard : BlackBoard
{
    public UnityEngine.Transform player;
    #region 检测相关参数
    [Header("墙壁检测相关")]
    // 设置墙壁检测光线检测距离
    public float RayToMonitorWall_Distance = 10f;
    // 获取检测墙壁层级掩码
    public LayerMask WallLayer;

    [Header("玩家检测相关")]
    // 设置扇形检测区域的角度
    public float MonitorDeg = 120f;
    // 设置扇形检测区域的半径
    public float MonitorRad = 15f;
    // 获取玩家的层级掩码
    public LayerMask PlayerLayer;
    // 获得射线检测对象
    protected RaycastHit hitObject;
    // 检测起始点的Y轴偏移量
    public float Yoffset = 0.5f;
    #endregion

    public float IdleTime;
    public StateType nextState;
    public float PatrolTime;
    // 移动速度
    public float movespeed = 8f;
    // 当前位置
    public UnityEngine.Transform curPos;
    #region 射线检测相关
    // 检测玩家
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
                    // 设置状态
                    // 设置敌人行为
                    curPos.LookAt(player.position);
                }
            }
        }
        
    }
    // 检测墙壁
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
    // 创建状态机
    private FSM PTE_fsm;
    // 创建黑板
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
        // 执行当前
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
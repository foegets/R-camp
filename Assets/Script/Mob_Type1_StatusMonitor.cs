using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Mob_Type1_StatusMonitor : PatrolTypeEnemy
{
    float tmp = 1;
    void Start()
    {
        PatrolBaseInitialize();
        CharacterBaseInitialize();
        EnemyBaseInitialize();
        ismove = true;
        isidle = false;
    }
    
    void Update()
    {
        if (HP < HP_Bar.value)
        {
            HP_Bar.value -= 0.5f;
        }

        // 检测死亡
        DeathDetect();

        #region 动画相关
        if (isdead)
        {
            animator.SetTrigger("isdead");
        }
        if (ismove && !isonbattle && !isdead)
        {
            animator.SetBool("isnormalmove", true);
        }
        else
        {
            animator.SetBool("isnormalmove", false);
        }
        if (ismove && isonbattle && !isdead)
        {
            animator.SetBool("isbattlemove", true);
        }
        else
        {
            animator.SetBool("isbattlemove", false);
        }
        if (isdefending && !isdead)
        {
            animator.SetBool("isdefending", true);
        }
        else
        {
            animator.SetBool("isdefending", false);
        }
        if (isattacking && !isdead)
        {
            animator.SetBool("isattacking", true);
        }
        else
        {
            animator.SetBool("isattacking", false);
        }
        if (isidle && !isdead)
        {
            animator.SetBool("isidle", true);
        }
        else
        {
            animator.SetBool("isidle", false);
        }
        #endregion

        #region 检测相关
        // 检测前方扇形区域内是否有玩家
        if (!isattacking && !isdead)
        {
            SearchPlayer();
        }
        // 检测前方是否有墙壁
        if (!isdead && isonpatrol && ismove)
        {
            DetectWall();
        }
        #endregion

        #region 巡逻相关状态
        if (isonpatrol && ismove && !isdead)
        {
            MoveOnPatrol();
        }
        #endregion

        #region 战斗相关状态
        // 进入战斗状态
        if (isonbattle && !isdead)
        {
            BattleMode();
            if (isattacking)
            {
                // 临时怎么搞的，主要没好好设计敌人AI
                tmp += Time.deltaTime;
                if ((int)tmp % 2 == 1)
                {
                    AttackDetect[0].SetActive(true);
                }
                else
                {
                    AttackDetect[0].SetActive(false);
                }
            }
            else
            {
                AttackDetect[0].SetActive(false);
            }
        }
        // 受到攻击后的应激状态
        if (!isonbattle && isgethit && !isdead)
        {
            UnderAttack();
        }
        #endregion

        #region 脱战相关状态
        // 进入脱战状态
        if (isoutbattle && !isdead)
        {
            OutBattleMode();
        }

        #endregion
    }

}

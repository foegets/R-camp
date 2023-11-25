using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Mob_Type1_StatusMonitor : PatrolTypeEnemy
{

    void Start()
    {
        PatrolBaseInitialize();
        CharacterBaseInitialize();
        EnemyBaseInitialize();
    }
    
    void Update()
    {
        // 检测死亡
        DeathDetect();

        #region 动画相关
        if (ismove && isonpatrol)
        {
            animator.SetBool("ismove_patrol", true);
        }
        else
        {
            animator.SetBool("ismove_patrol", false);
        }
        if (ismove && isonbattle)
        {
            animator.SetTrigger("ismove_battle");
        }
        if (isonbattle)
        {
            animator.SetBool("isonbattle", true);
        }
        else
        {
            animator.SetBool("isonbattle", false);
        }
        if (isdefending)
        {
            animator.SetBool("isdefending", true);
        }
        else
        {
            animator.SetBool("isdefending", true);
        }
        if (!isonbattle && !isidle && !isonpatrol)
        {
            animator.SetBool("isreturning", true);
        }
        else
        {
            animator.SetBool("isreturning", false);
        }
        #endregion

        #region 检测相关
        // 检测前方扇形区域内是否有玩家
        if (!isattacking && !isdead)
        {
            SearchPlayer();
        }
        // 检测前方是否有墙壁
        if (!isdead && isonpatrol)
        {
            DetectWall();
        }
        #endregion

        #region 巡逻相关状态
        if (isonpatrol)
        {
            MoveOnPatrol();
        }
        #endregion

        #region 战斗相关状态
        // 朝向玩家，准备战斗
        if (isfindplayer && !isdead && !isonbattle && !isonpatrol)
        {
            PrepareBattle();
        }

        // 进入战斗状态
        if (isonbattle && !isdead)
        {
            BattleMode();
        }
        // 受到攻击后的应激状态
        if (!isonbattle && isgethit && !isdead)
        {
            isonbattle = true;
            isdefending = true;
        }
        #endregion

        #region 脱战相关状态
        // 进入准备脱战状态
        if (isidle && isonbattle && ElapedTime >= AggroLastTime + 1 && !isdead)
        {
            isonbattle = false;
            isidle = false;

        }
        // 进入脱战状态
        if (!isonbattle && !isonpatrol && !isdead)
        {
            OutBattleMode();
        }

        #endregion
    }

}

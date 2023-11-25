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
        // �������
        DeathDetect();

        #region �������
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

        #region ������
        // ���ǰ�������������Ƿ������
        if (!isattacking && !isdead)
        {
            SearchPlayer();
        }
        // ���ǰ���Ƿ���ǽ��
        if (!isdead && isonpatrol)
        {
            DetectWall();
        }
        #endregion

        #region Ѳ�����״̬
        if (isonpatrol)
        {
            MoveOnPatrol();
        }
        #endregion

        #region ս�����״̬
        // ������ң�׼��ս��
        if (isfindplayer && !isdead && !isonbattle && !isonpatrol)
        {
            PrepareBattle();
        }

        // ����ս��״̬
        if (isonbattle && !isdead)
        {
            BattleMode();
        }
        // �ܵ��������Ӧ��״̬
        if (!isonbattle && isgethit && !isdead)
        {
            isonbattle = true;
            isdefending = true;
        }
        #endregion

        #region ��ս���״̬
        // ����׼����ս״̬
        if (isidle && isonbattle && ElapedTime >= AggroLastTime + 1 && !isdead)
        {
            isonbattle = false;
            isidle = false;

        }
        // ������ս״̬
        if (!isonbattle && !isonpatrol && !isdead)
        {
            OutBattleMode();
        }

        #endregion
    }

}

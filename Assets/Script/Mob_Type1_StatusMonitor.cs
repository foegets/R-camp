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

        // �������
        DeathDetect();

        #region �������
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

        #region ������
        // ���ǰ�������������Ƿ������
        if (!isattacking && !isdead)
        {
            SearchPlayer();
        }
        // ���ǰ���Ƿ���ǽ��
        if (!isdead && isonpatrol && ismove)
        {
            DetectWall();
        }
        #endregion

        #region Ѳ�����״̬
        if (isonpatrol && ismove && !isdead)
        {
            MoveOnPatrol();
        }
        #endregion

        #region ս�����״̬
        // ����ս��״̬
        if (isonbattle && !isdead)
        {
            BattleMode();
            if (isattacking)
            {
                // ��ʱ��ô��ģ���Ҫû�ú���Ƶ���AI
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
        // �ܵ��������Ӧ��״̬
        if (!isonbattle && isgethit && !isdead)
        {
            UnderAttack();
        }
        #endregion

        #region ��ս���״̬
        // ������ս״̬
        if (isoutbattle && !isdead)
        {
            OutBattleMode();
        }

        #endregion
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolTypeEnemy : Enemy_Base
{
    [Header("��ʱ�����")]
    // ��������ʱ��
    public float AttackLastTime = 2.2f;
    // ��޳���ʱ��
    public float AggroLastTime = 3f;
    // ��սվ������ʱ��
    public float OutBatElaped = 1f;
    // Ѳ��վ������ʱ��
    public float PatrolElaped = 2.5f;
    // ��������ʱ��
    public float DefendLastTime = 3f;
    // ս��׼��ʱ��
    public float BatPrepareTime = 1f;

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

    [Header("����״̬")]
    // ����Ƿ���Ѳ��״̬
    public bool isonpatrol;
    // ���ǰ���Ƿ���ǽ
    public bool isfrontexitwall;
    // �ж��Ƿ������
    public bool isfindplayer;
    // �Ƿ��ڷ���״̬
    public bool isdefending;
    // �Ƿ�����ս״̬
    public bool isoutbattle;

    // ���AI������
    NavMeshAgent AiGuide;

    // Э�����
    // Ѳ�����Э��
    protected Coroutine PatrolCoroutine;

    void Start()
    {
        PatrolBaseInitialize();
    }

    
    // ��ʼ��
    protected void PatrolBaseInitialize()
    {
        isonpatrol = true;
        isfrontexitwall = false;
        isfindplayer = false;
        isdefending = false;
        isoutbattle = false;
        AiGuide = GetComponent<NavMeshAgent>();
    }
    // ��սģʽ
    protected void OutBattleMode()
    {
        // ����״̬
        SetMoveStatus(false, true, false, false);
        // �ص���ʼλ��
        // ������������֮��ƫ����
        Vector3 offset = originalpos - transform.position;
        // �����������ҵ�Ҫ��ת�ĽǶ�
        Quaternion targetRot = Quaternion.LookRotation(offset);
        // ʹ��ƽ����ֵ���㵱ǰҪ��ת�ĽǶ�
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, smoothing);
        // �����ʼλ��ǰ��
        transform.position += transform.forward * movespeed * 1.2f * Time.deltaTime;
        if (Vector3.Distance(transform.position, originalpos) <= 0.8f)
        {
            isonpatrol = true;
            transform.position = originalpos;
            transform.rotation = originalrot;
            SetMoveStatus(true, false, false, false);
            StartCoroutine(OutBattleDelayTime());
        }
    }
    // ս��ģʽ
    protected void BattleMode()
    {
        
        float Distance = Vector3.Distance(transform.position, player.position);
        // ��¼��ǰ��ת
        Quaternion currentRot = transform.rotation;

        // ������̬
        if (isdefending && !isattacking)
        {
            ElapedTime = Time.time - MarkTimer;
            if (ElapedTime >= DefendLastTime)
            {
                SetMoveStatus(true, false, false, false);
                if (isfindplayer)
                {
                    transform.LookAt(player.position);
                }
                else
                {
                    // ����׼����ս״̬
                    SetModeStatus(false, false, true);
                }
                ElapedTime = 0;
            }
            else
            {
                if (isfindplayer)
                {
                    transform.LookAt(player.position);
                }
            }
            //AiGuide.SetDestination(player.forward);
        }
        else if (Distance <= 5f && !isattacking)
        {
            // ����ҽ��й���
            transform.LookAt(player.position);
            SetMoveStatus(false, false, true, false);
            MarkTimer = Time.time;
        }
        else if (Distance > 5f && !isattacking)
        {
            // ����׷��״̬
            PursueMode(currentRot);
            SetMoveStatus(false, true, false, false);
            //AiGuide.speed = movespeed * 1.2f;
            //AiGuide.SetDestination(player.transform.position);
        }
        // ����ing
        else if (isattacking)
        {
            ElapedTime = Time.time - MarkTimer;
            if (ElapedTime >= AttackLastTime)
            {
                // ��������һ��ǰ���Ƿ��е���
                // ��û�������׼����ս״̬
                // ��������������̬
                SearchPlayer();
                if (isfindplayer)
                {
                    MarkTimer = Time.time;
                    SetMoveStatus(false, false, false, true);
                }
                else
                {
                    SetModeStatus(false, false, true);
                    SetMoveStatus(true, false, false, false);
                    ElapedTime = 0;
                }
            }
        }

        // ��޼�ʱ��
        if (!isfindplayer && ElapedTime >= AggroLastTime && !isattacking)
        {
            SetMoveStatus(true, false, false, false);
            SetModeStatus(false, false, true);
            ElapedTime = 0;
        }
    }
    // �ܵ��������Ӧ����Ӧ
    protected void UnderAttack()
    {
        SetModeStatus(false, true, false);
        SetMoveStatus(false, false, false, true);
        MarkTimer = Time.time;
    }
    
    // Ѳ������
    protected void MoveOnPatrol()
    {   
        transform.position += transform.forward * movespeed * Time.deltaTime;
    }

    #region ���ʱ�����
    // ��սվ�����ʱ��
    IEnumerator OutBattleDelayTime()
    {
        yield return OutBatElaped;
        SetModeStatus(true, false, false);
        SetMoveStatus(false, true, false, false);
    }
    // Ѳ��վ�����ʱ��
    IEnumerator PatrolDelayTime()
    {
        yield return PatrolElaped;
        SetMoveStatus(false, true, false, false);
    }
    // ս��׼��ʱ��
    IEnumerator BattleDelayTime()
    {
        yield return BatPrepareTime;
        SetModeStatus(false, true, false);
    }
    #endregion
    #region ����״̬���
    // �ĸ��������״̬
    protected void SetMoveStatus(bool idleActive, bool moveActive, bool attackActive, bool defendActive)
    {
        ismove = moveActive;
        isidle = idleActive;
        isattacking = attackActive;
        isdefending = defendActive;
    }
    // ������Ϊģʽ���״̬
    protected void SetModeStatus(bool patrolActive, bool battleAtive, bool outbatActive)
    {
        isonpatrol = patrolActive;
        isonbattle = battleAtive;
        isoutbattle = outbatActive;
    }
    #endregion

    #region ׷��ģʽ
    protected void PursueMode(Quaternion currentRot)
    {
        // ������������֮��ƫ����
        Vector3 offset = player.position - transform.position;
        // �����������ҵ�Ҫ��ת�ĽǶ�
        Quaternion targetRot = Quaternion.LookRotation(offset);
        // ʹ��ƽ����ֵ���㵱ǰҪ��ת�ĽǶ�
        currentRot = Quaternion.Lerp(currentRot, targetRot, smoothing);
        transform.rotation = currentRot;
        // �������ǰ��
        transform.position += transform.forward * movespeed * 1.2f * Time.deltaTime;
    }

    #endregion

    #region ���߼�����
    // ������
    protected void SearchPlayer()
    {
        float rightangle = transform.rotation.eulerAngles.y + MonitorDeg / 2;
        float leftangle = transform.rotation.eulerAngles.y - MonitorDeg / 2;
        for (float i = leftangle; i <= rightangle; i += 2)
        {
            Vector3 MonitorDir = new Vector3(Mathf.Sin(i * Mathf.Deg2Rad), transform.position.y + Yoffset, Mathf.Cos(i * Mathf.Deg2Rad));
            if (Physics.Raycast(transform.position + new Vector3(0, Yoffset, 0), MonitorDir, out hitObject, MonitorRad, PlayerLayer))
            {
                if (hitObject.collider.CompareTag("Player"))
                {
                    // ���ʱ���
                    MarkTimer = Time.time;
                    // ����״̬
                    isfindplayer = true;
                    isonpatrol = false;
                    // ���õ�����Ϊ
                    transform.LookAt(player.position);
                    SetMoveStatus(true, false, false, false);
                    StartCoroutine(BattleDelayTime());
                    StopCoroutine(PatrolCoroutine);
                }
            }   
        }
        if (hitObject.collider == null)
        {
            isfindplayer = false;
        }
    }
    // ���ǽ��
    protected void DetectWall()
    {
        if(Physics.Raycast(transform.position + new Vector3(0, Yoffset, 0), transform.forward + new Vector3(0, Yoffset, 0), RayToMonitorWall_Distance, WallLayer))
        {
            SetMoveStatus(true, false, false, false);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
            PatrolCoroutine = StartCoroutine(PatrolDelayTime());
        }
    }
    #endregion

}

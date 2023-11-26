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

    // ���AI������
    NavMeshAgent AiGuide; 

    void Start()
    {
        PatrolBaseInitialize();
    }

    
    void Update()
    {

    }
    // ��ʼ��
    protected void PatrolBaseInitialize()
    {
        isonpatrol = true;
        isfrontexitwall = false;
        isfindplayer = false;
        isdefending = false;

        AiGuide = GetComponent<NavMeshAgent>();
    }
    // ��սģʽ
    protected void OutBattleMode()
    {
        // �ص���ʼλ��
        // ������������֮��ƫ����
        Vector3 offset = originalpos - transform.position;
        // �����������ҵ�Ҫ��ת�ĽǶ�
        Quaternion targetRot = Quaternion.LookRotation(offset);
        // ʹ��ƽ����ֵ���㵱ǰҪ��ת�ĽǶ�
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, smoothing);
        // �����ʼλ��ǰ��
        transform.position += transform.forward * movespeed * 1.2f * Time.deltaTime;
        SetBasicStatus(false, true, false);
        if (Vector3.Distance(transform.position, originalpos) <= 0.1f)
        {
            isonpatrol = true;
            transform.position = originalpos;
            transform.rotation = originalrot;
            SetBasicStatus(true, false, false);
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
                isdefending = false;
                if (isfindplayer)
                {
                    transform.LookAt(player.position);
                    StartCoroutine(AttackDelayTime());
                }
                else
                {
                    // ����׼����ս״̬
                    SetBasicStatus(true, false, false);
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
            SetBasicStatus(false, false, false);
            MarkTimer = Time.time;
        }
        else if (Distance > 5f && !isattacking)
        {
            // ����׷��״̬
            PursueMode(currentRot);
            SetBasicStatus(false, true, false);
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
                    isdefending = true;
                    MarkTimer = Time.time;
                    SetBasicStatus(false, true, false);
                }
                else
                {
                    SetBasicStatus(true, false, false);
                    ElapedTime = 0;
                }
            }
        }

        // ��޼�ʱ��
        if (!isfindplayer && ElapedTime >= AggroLastTime && !isattacking)
        {
            SetBasicStatus(true, false, false);
            ElapedTime = 0;
        }
    }
    // �ܵ��������Ӧ����Ӧ
    protected void UnderAttack()
    {
        // �ܵ��������Ӧ��״̬
        if (!isonbattle && isgethit && !isdead)
        {
            isonbattle = true;
            isdefending = true;
            MarkTimer = Time.time;
        }
    }
    // ս��׼��
    protected void PrepareBattle()
    {
        transform.LookAt(player.position);
        SetBasicStatus(true, false, false);
        StartCoroutine(AttackDelayTime());
        isonbattle = true;
    }
    // Ѳ��
    protected void MoveOnPatrol()
    {
        SetBasicStatus(false, true, false);
        transform.position += transform.forward * movespeed * Time.deltaTime;
    }

    #region ���ʱ�����
    // �������ʱ��
    IEnumerator AttackDelayTime()
    {
        yield return AttackElaped;
    }
    // ��սվ�����ʱ��
    IEnumerator OutBattleDelayTime()
    {
        yield return OutBatElaped;
    }
    // Ѳ��վ�����ʱ��
    IEnumerator PatrolDelayTime()
    {
        yield return PatrolElaped;
    }
    #endregion
    #region ��������״̬
    protected void SetBasicStatus(bool idleActive, bool moveActive, bool attackActive)
    {
        ismove = moveActive;
        isidle = idleActive;
        isattacking = attackActive;
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
                    MarkTimer = Time.time;
                    isfindplayer = true;
                    isonpatrol = false;
                }
            }
        }
    }
    // ���ǽ��
    protected void DetectWall()
    {
        if(Physics.Raycast(transform.position + new Vector3(0, Yoffset, 0), transform.forward + new Vector3(0, Yoffset, 0), RayToMonitorWall_Distance, WallLayer))
            {
            SetBasicStatus(true, false, true);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
            StartCoroutine(PatrolDelayTime());
        }
    }
    #endregion

}

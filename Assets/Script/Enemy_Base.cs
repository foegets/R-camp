using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base : Character_BasicStatus
{
    [Header("��ȡ��Ҷ���")]
    // �����Ҷ���
    public Transform player;

    [Header("��ʱ�����")]
    public float AttackElaped = 0.5f;

    [Header("����״̬")]
    // �ж��Ƿ�ӽ����
    public bool iscloseplayer;
    // �ж��Ƿ��ܵ�����
    public bool isgethit;
    // �ж��Ƿ���ս��״̬
    public bool isonbattle;
    // �ж��Ƿ��ڽ��й���
    public bool isattacking;
    // �ж��Ƿ���վ��״̬
    public bool isidle;
    // �ж��Ƿ����ƶ�״̬
    public bool ismove;
    void Start()
    {
        EnemyBaseInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void EnemyBaseInitialize()
    {
        isidle = false;
        isonbattle = false;
        iscloseplayer = false;
        isgethit = false;
        isattacking = false;
        ismove = false;
    }
}

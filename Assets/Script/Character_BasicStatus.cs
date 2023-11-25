using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_BasicStatus : MonoBehaviour
{
    [Header("Ѫ�����")]
    // ���Ѫ��
    public UnityEngine.UI.Slider HP_Bar;
    // �������Ѫ��
    public float HP = 100;

    // ��ö�����
    protected Animator animator;

    [Header("�ٶ����")]
    // �ƶ��ٶ�
    public float movespeed = 8f;
    // ��ת�ٶ�
    public float rotatespeed = 15f;
    // ƽ����ֵ
    public float smoothing = 0.2f;

    [Header("��ʱ�����")]
    // ���ʱ��
    public float ElapedTime;
    // ���ʱ���
    public float MarkTimer;

    // ��ó�ʼλ��
    protected Vector3 originalpos;
    // ��ó�ʼ��ת
    protected Quaternion originalrot;
    // ��ó�ʼ����
    protected Vector3 originalscale;

    [Header("����״̬")]
    // �ж��Ƿ�����
    public bool isdead;
    void Start()
    {
        CharacterBaseInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        // �������
        DeathDetect();
    }

    protected void CharacterBaseInitialize()
    {
        isdead = false;

        animator = GetComponent<Animator>();

        originalpos = transform.position;
        originalrot = transform.rotation;
        originalscale = transform.localScale;

        HP_Bar.maxValue = HP;
        HP_Bar.minValue = 0;
        HP_Bar.value = HP_Bar.maxValue;

        MarkTimer = 0f;
        ElapedTime = 0f;
    }

    protected void DeathDetect()
    {
        if (HP_Bar.value == HP_Bar.minValue)
        {
            isdead = true;
            animator.SetTrigger("isdead");
        }
    }
}

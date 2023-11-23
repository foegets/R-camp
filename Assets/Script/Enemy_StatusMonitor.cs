using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Enemy_StatusMonitor : MonoBehaviour
{
    [Header("��ȡ��Ҷ���")]
    // �����Ҷ���
    public Transform player;
    // ��ö�����
    Animator animator;
    [Header("Ѫ�����")]
    // ���Ѫ��
    public UnityEngine.UI.Slider HP_Bar;
    public float HP = 100;
    [Header("ǽ�ڼ�����")]
    // ����ǽ�ڼ����߼�����
    public float RayToMonitorWall_Distance = 10f;
    // ��ȡ���ǽ�ڲ㼶����
    public LayerMask WallLayer;
    // ������߼�����
    RaycastHit hitObject;
    [Header("�ٶ����")]
    // �ƶ��ٶ�
    public float movespeed = 10f;
    
    
    [Header("��Ҽ�����")]
    // �������μ������ĽǶ�
    public float MonitorDeg = 120f;
    // �������μ������İ뾶
    public float MonitorRad = 15f;
    // ��ȡ��ҵĲ㼶����
    public LayerMask PlayerLayer;
       
    // ��ʱ��
    float Timer;
    [Header("���ʱ��")]
    // ���ʱ��
    public float ElapedTime;

    // ��ó�ʼλ��
    Vector3 originalpos;
    [Header("����״̬")] 
    // �ж��Ƿ�����
    public bool isdead;
    // �ж��Ƿ���ս��״̬
    public bool isonbattle;
    // ����Ƿ���Ѳ��״̬
    public bool isonpatrol;
    // ���ǰ���Ƿ���ǽ
    public bool isfrontexitwall;
    // �ж��Ƿ������
    public bool isfindplayer;
    // �ж��Ƿ�ӽ����
    public bool iscloseplayer;
    // �ж��Ƿ��ܵ�����
    public bool isgethit;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        originalpos = transform.position;
        HP_Bar.maxValue = HP;
        HP_Bar.minValue = 0;
        HP_Bar.value = HP;
        isdead = false;
        isonpatrol = true;
        isonbattle = false;
        isfrontexitwall = false;
        Timer = 0f;
        ElapedTime = 0f;
        isfindplayer = false;
        iscloseplayer = false;
        isgethit = false;
    }

    private void FixedUpdate()
    {
       
    }
    
    void Update()
    {
        
        if (HP_Bar.value == 0)
        {
            isdead = true;
            animator.SetTrigger("isdead");
        }
        // ����ing
        if (isonbattle && iscloseplayer)
        {
            ElapedTime = Time.time - Timer;
            if (ElapedTime >= 2.2f)
            {
                iscloseplayer = false;
                ElapedTime = 0;
                animator.SetBool("iscloseplayer", false);
                animator.SetBool("isfindplayer", false);
            }
        }
        // ���ǰ�����������Ƿ������
        float rightangle = transform.rotation.eulerAngles.y + MonitorDeg / 2;
        float leftangle = transform.rotation.eulerAngles.y - MonitorDeg / 2;
        for (float i = leftangle; i <= rightangle; i+= 2)
        {
            Vector3 MonitorDir = new Vector3(Mathf.Sin(i * Mathf.Deg2Rad), transform.position.y + 0.5f, Mathf.Cos(i * Mathf.Deg2Rad));
            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), MonitorDir, out hitObject, MonitorRad, PlayerLayer))
            {
                if (hitObject.collider.CompareTag("Player"))
                {
                    Timer = Time.time;
                    isfindplayer = true;
                    isonbattle = true;
                    isonpatrol = false;
                }
            }
            
        }
        if (isgethit)
        {
            Timer = Time.time;
            isfindplayer = true;
            isonpatrol = false;
            isonbattle = true;
        }
        // ����׷��ʱ��
        if (isfindplayer)
        {
            if (ElapedTime >= 4f && !iscloseplayer)
            {
                animator.SetBool("isfindplayer", false);
                animator.SetBool("iscloseplayer", false);
                isonpatrol = true;
                isonbattle = false;
                isfindplayer = false;
                transform.position = originalpos;
                transform.eulerAngles = new Vector3(0, 90, 0);
                ElapedTime = 0;
            }
        }
        // ���ǰ���Ƿ����ϰ���
        // ����ǽ�ڼ�����
        Ray DistanceMonitorRay = new Ray(transform.position + Vector3.up, transform.forward);
        // �������
        isfrontexitwall = Physics.Raycast(DistanceMonitorRay, RayToMonitorWall_Distance, WallLayer);
        // վ��ģʽ
        if (!isonpatrol && !isdead && !isonbattle)
        {
            animator.SetBool("ismove_patrol", false);
            ElapedTime = Time.time - Timer;
            if (ElapedTime > 3.5f)
            {
                isonpatrol = true;
                ElapedTime = 0;
            }
        }
        // Ѳ��ģʽ
        if (isonpatrol && !isdead)
        {
            Timer = Time.time;
            transform.position += transform.forward * movespeed * Time.deltaTime;
            animator.SetBool("ismove_patrol", true);
            if (isfrontexitwall)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
                isonpatrol = false;
            }
        }
        // ս��ģʽ
        else if (isonbattle && !isdead && !iscloseplayer)
        {
            animator.SetBool("isfindplayer", true);
            ElapedTime = Time.time - Timer;
            transform.LookAt(player.position);
            transform.position += transform.forward * movespeed * 1.2f * Time.deltaTime;
            if (Vector3.Distance(player.position, transform.position) <= 4f)
            {
                Timer = Time.time;
                iscloseplayer = true;
                animator.SetBool("iscloseplayer", true);
            }
            else
            {
                animator.SetBool("iscloseplayer", false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;//�����������ڻ��ͨ���ٶ��л�������animator
    private Rigidbody2D rb;//�����������ڻ���ٶ�������velocityX
    private PlayerController playerController;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        //�����ٶȺ�ȡ����ֵ�����������ٶ�Ϊ�����޷�ת������������
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        anim.SetBool("isDead", playerController.isDead);
    }

    //ʵ�����˺󲥷���˸����
    public void PlayHurt()
    {
        anim.SetTrigger("hurt trigger");       
    }
}

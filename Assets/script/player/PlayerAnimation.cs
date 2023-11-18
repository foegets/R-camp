using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;//�����������ڻ��ͨ���ٶ��л�������animator
    private Rigidbody2D rb;//�����������ڻ���ٶ�������velocityX
    private PlayerController playerController;
    private PhysicsCheck physicsCheck;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }
    private void Update()
    {
        SetAnimation();
    }

    //�ò���������PlayerController�е�ֵ��Animaition�еĸ��ֱ�����ʵ�ּ�ͷ�ж������ж�
    public void SetAnimation()
    {
        //�����ٶȺ�ȡ����ֵ�����������ٶ�Ϊ�����޷�ת������������
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isDead", playerController.isDead);
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isAttack", playerController.isAttack);

    }

    //ʵ�����˺󲥷Ŷ���
    public void PlayHurt()
    {
        anim.SetTrigger("hurt trigger");   
    }

    //ʵ�ְ��°����󲥷Ź�������
    public void PlayerAttack()
    {
        anim.SetTrigger("attack trigger");
    }


}

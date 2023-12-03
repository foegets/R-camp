using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;

    private PhysicsCheck physicsCheck;

    private PlayerController playerController;


    private void Awake(){
        anim=GetComponent<Animator>();//GetComponent�ǻ�ȡ��ǰ��Ϸ��������ķ���������ͨ��ֱ�ӵ�������������Ϸ���������ͽ��в�������
        rb = GetComponent<Rigidbody2D>();
        physicsCheck=GetComponent<PhysicsCheck>();
        playerController=GetComponent<PlayerController>();
    }

    private void Update(){
        SetAnimation();
    }

    public void SetAnimation(){
        //��PlayerController�еı�����animator�����ж���������ֵ��������,��׼ȷ��˵�Ǹ�ֵ��
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY",rb.velocity.y);
        anim.SetBool("isGround",physicsCheck.isGround);
        anim.SetBool("isDead",playerController.isDead);
    }

    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }

}

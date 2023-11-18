using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public CapsuleCollider2D coll;
    [Header("������")]
    public float checkRaduis;//������Χ����������
    public LayerMask groundLayer;//ɸѡ����ѡ�����������󣬲���
    public Vector2 bottonOffset;//�������Բ��Բ��λ�ã�����ȷ����ײ��ʵ�ʼ�ⷶΧ��һ������xy��������ı���
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    [Header("״̬")]
    public bool isGround;//���������ж��Ƿ��������ײ�ı���
    //�����ж��Ƿ���ǽ��Ϊ���������ǽת��Ͷ���ת����׼����
    public bool isLeftWall;
    public bool isRightWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        check();
    }
    public void check()
    {
        //����,ǽ�ڼ�⣨��Բ���ж������أ�ͬ������ͬ��,��һ����������λ�ñ仯�ͽŵ�λ�Ʋ�ֵ����ײ���ʽΪtrue
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottonOffset,checkRaduis,groundLayer);
        isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
    }


    
    //����isGround�ļ�ⷶΧ���ͨ��unity�Դ�����gizmos,��Բ�α�ʾ���ֶ�����
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottonOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}

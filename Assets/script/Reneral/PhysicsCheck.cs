using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public float checkRaduis;//������Χ����������
    public LayerMask groundLayer;//ɸѡ����ѡ�����������󣬲���
    public Vector2 bottonOffset;//�ŵ�λ�Ʋ�ֵ������ȷ����ײ��ʵ�ʼ�ⷶΧ
    [Header("״̬")]
    public bool isGround;//���������ж��Ƿ��������ײ�ı���
    private void Update()
    {
        check();
    }
    public void check()
    {
        //�����⣨��Բ���ж������أ�ͬ������ͬ��,��һ����������λ�ñ仯�ͽŵ�λ�Ʋ�ֵ����ײ���ʽΪtrue
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottonOffset,checkRaduis,groundLayer);
    }
    //���ʵ�ʱ������gizmos,��Բ�α�ʾ
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottonOffset, checkRaduis);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ô�������ʵʩ����
public class Attack : MonoBehaviour
{
    public int damage;//�����˺�
    public float attackRange;//�˺���Χ
    public float attackRate;//�˺�Ƶ��
    //�Գ����������ڽ���ͳ�ȥ���ķ�ʽ
    private void OnTriggerStay2D(Collider2D other)
    {   
        //��ȡcalculate��������ʱ������ߣ��������빥����ֵ,?��ʾ�Ƚ����Ƿ��й���������ж�
        other.GetComponent<Calculate>()?.TakeDamage(this);
    }

}

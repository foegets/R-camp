using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy//�е��ǡ�����ļ̳У�
{
    public override void Move()//��д/����Move����
    {
        base.Move();//����ԭ����Move����������
        anim.SetBool("walk", true);//ʹAnimator�е�walk=true��ʹ��boarWalk������ʼ����

    }
}

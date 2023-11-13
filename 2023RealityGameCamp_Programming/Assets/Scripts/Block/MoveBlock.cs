using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public float speed = 2f; // �ƶ��ٶ�
    public float distance = 3f; // �ƶ�����
    public bool moveUp = true; // �Ƿ������ƶ�

    private Vector2 startPos; // ��ʼλ��
    private Vector2 endPos; // ����λ��

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector2(0, distance);
    }

    private void Update()
    {
        if (moveUp)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y >= endPos.y)
            {
                moveUp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            if (transform.position.y <= startPos.y)
            {
                moveUp = true;
            }
        }
    }
}

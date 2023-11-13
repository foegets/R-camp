using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public float speed = 2f; // 移动速度
    public float distance = 3f; // 移动距离
    public bool moveUp = true; // 是否向上移动

    private Vector2 startPos; // 起始位置
    private Vector2 endPos; // 结束位置

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

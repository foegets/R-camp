using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public enum PlatformType
{
    x,y,s   //x为在x轴上移动,y为在y轴上移动,s为静止平台
}

public class MovablePlatform : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movingSpeed;
    public int direction = 1;
    public PlatformType type;
    [SerializeField]private Vector2 initalPosition;
    public float moveRange;
 

    private void Awake()
    {
        initalPosition = transform.position;
    }



    private void FixedUpdate()
    {
        PlatformMove(type);
    }


    public void PlatformMove(PlatformType type)
    {
        this.type = type;
        switch(type) 
        {
            case PlatformType.x:
                float positionx = transform.position.x;

                if (positionx < initalPosition.x - moveRange)
                    direction = 1;
                if (positionx > initalPosition.x + moveRange)
                    direction = -1;
                transform.position = new Vector2(transform.position.x+ movingSpeed * Time.fixedDeltaTime * direction,transform.position.y);
                break;

            case PlatformType.y:
                float positiony = transform.position.y;

                if (positiony < initalPosition.y - moveRange)
                    direction = 1;
                if (positiony > initalPosition.y + moveRange)
                    direction = -1;
                transform.position = new Vector2(transform.position.x,transform.position.y+ movingSpeed * Time.fixedDeltaTime*direction);
                break;

            case PlatformType.s:
                break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (type)
        {
            case PlatformType.x:
                collision.transform.position = new Vector2(collision.transform.position.x + movingSpeed * Time.fixedDeltaTime * direction, collision.transform.position.y);
                break;
            case PlatformType.y:
                collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + movingSpeed * Time.fixedDeltaTime * direction);
                break;
            case PlatformType.s:
                break;
        }
        
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatformController : MonoBehaviour//基本与敌人移动一致
{
    public GameObject platformPoint1;
    public GameObject platformPoint2;
    private Rigidbody2D platformRb;
    private int direction = 1;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
    }
    public void Flip()
    {
        if(Vector2.Distance(transform.position, platformPoint2.transform.position)<0.5f)
        {
            direction=-1;
        }
        if(Vector2.Distance(transform.position, platformPoint1.transform.position)<0.5f)
        {
            direction=1;
        }
    }
    public void Move()
    {
        platformRb.velocity = new Vector2(0, moveSpeed * direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlP : MonoBehaviour
{
    public float speed;
    public float jump;
    public bool isGround;//检测人物在地面上
    public LayerMask Ground;//绑定图层
    public Rigidbody2D rbody;
    public Collider2D Coll;
    public Animator playerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        float horizontalNum = Input.GetAxis("Horizontal");
        rbody.velocity = new Vector2(speed * horizontalNum,rbody.velocity.y);
        float faceNum = Input.GetAxisRaw("Horizontal");
        playerAnim.SetFloat("run", Mathf.Abs(speed * horizontalNum));
        if(faceNum != 0)
        {
            transform.localScale = new Vector3(-faceNum, transform.localScale.y, transform.localScale.z);
        }
    }
    void Jump()
    {
        if(Input.GetButton("jump"))
        {
             rbody.velocity = new Vector2(rbody.velocity.x,jump);
        }
       
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("速度相关")]
    public float playmovespeed;
    public float playerJumpSpeed;
    [Header("跳跃次数")]
    public float playerJumpCount;
    [Header("判断相关")]
    public bool isGround;
    public bool isJump;
    public bool pressedJump;
    [Header("其他组件")]
    public Transform foot;
    public LayerMask Ground;
    public Rigidbody2D playerRB;
    public Collider2D playColl;
    public Animator PlayerAnim;

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(10, 10, 300, 100), "coin Num:" + coin.coinCount);

    }


    void Start()
    {
        playColl = GetComponent<Collider2D>();              //让物体能产生碰撞 不会掉
        playerRB = GetComponent<Rigidbody2D>();             
        PlayerAnim = GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {
        UpdateCheck();
        AnimSwitch();   
    }
    private void FixedUpdate()
    {
        PlayMove();                                      //一直让他能够移动
        PlayerJump();                                    //跳跃
        FixupdateCheck();


    }
    void PlayMove()
    {
        float horizontalNum = Input.GetAxis("Horizontal");      //移动
        float faceNum = Input.GetAxisRaw("Horizontal");    //改变移动过程面部朝向
        playerRB.velocity = new Vector2(playmovespeed * horizontalNum, playerRB.velocity.y);
        PlayerAnim.SetFloat("run", Mathf.Abs(playmovespeed * horizontalNum)); //确保停止的时候动画改变
        if(faceNum!=0)   //改变面部朝向
        {
            transform.localScale = new Vector3(-faceNum, transform.localScale.y, transform.localScale.z);
        }

    }
    void PlayerJump()
    {
        
        if(isGround)
        {
            playerJumpCount = 1;
            isJump = false;
        }
        if(pressedJump && isGround)        
        {
            pressedJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);
            playerJumpCount--;
        }
        else if ((pressedJump && playerJumpCount>0&& !isGround))   //在空中
        {
            pressedJump = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);  //可以二连跳
            playerJumpCount--;
        }
       
    }
    void FixupdateCheck()
    {
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, Ground);
    }
    void UpdateCheck()
    {
        if (Input.GetButtonDown("Jump"))
        {
            pressedJump = true;
        }
    }
        void AnimSwitch()
        {
            if (isGround)
            {
                PlayerAnim.SetBool("jump", false);
            }
            if(!isGround&&playerRB.velocity.y!=0 )
            {
                PlayerAnim.SetBool("jump", true);
            }
        }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ThirdPersonMovement : MonoBehaviour
{
    //CharacterController playercontroller;
    // 获得目标摄像机
    public Transform thirdCam;
    // 设置移动速度
    public float movespeed = 20.0f;
    // 移动方向
    Vector3 movedir;
    // 设置旋转角度
    float targetangle;
    // 平滑旋转阻尼系数
    public float smoothrottime = 0.1f;
    // 没看懂这个的用处qwq
    float turnSmoothVelocity;
    //// 判断是否跳跃
    //bool isjumping;
    ////跳跃速度
    //public float jumpspeed = 10.0f;
    ////跳跃时间
    //public float jumpTime = 0.5f;
    ////累计跳跃时间用来判断是否结束跳跃
    //public float jumpTimeFlag = 0;
    //// 重力力度
    //public float gravity = -10.0f;
    // 判断是否在移动
    public bool ismove;
    // 玩家朝向
    Vector3 forwarddir;
    // 判断玩家是否在进行攻击
    public bool isAttack;
    void Start()
    {
        //isjumping = false;
        //playercontroller = GetComponent<CharacterController>();
        // 锁定并隐藏光标
        Cursor.lockState = CursorLockMode.Locked;
        isAttack = false;
    }

    private void FixedUpdate()
    {
        if (ismove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {

                transform.Translate(forwarddir.normalized * movespeed * 2 * Time.deltaTime, Space.World);
                //playercontroller.Move(forwarddir.normalized * movespeed * 2 * Time.deltaTime);
            }
            else
            {

                transform.Translate(forwarddir.normalized * movespeed * Time.deltaTime, Space.World);
                //playercontroller.Move(forwarddir.normalized * movespeed * Time.deltaTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        isAttack = GetComponent<PlayerBattleModeAnimationController>().isAttacking;
        //if (Input.GetKeyDown(KeyCode.Space) && !isjumping)
        //{
        //    isjumping = true;
        //}

        //if (isjumping)
        //{
        //    if (jumpTimeFlag < jumpTime)
        //    {
        //        playercontroller.Move(transform.up * jumpspeed * Time.deltaTime);
        //        jumpTimeFlag += Time.deltaTime;
        //    }
        //    else if (jumpTime < jumpTimeFlag)
        //    {
        //        playercontroller.Move(transform.up * gravity * Time.deltaTime);
        //    }
        //    if (playercontroller.collisionFlags == CollisionFlags.Below)
        //    {
        //        jumpTimeFlag = 0;
        //        isjumping = false;
        //    }
        //}
        //else
        //{
        //    if (playercontroller.collisionFlags != CollisionFlags.Below)
        //    {
        //        playercontroller.Move(transform.up * gravity * Time.deltaTime);
        //    }
        //}
        // 获取玩家输入
        movedir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // 计算玩家应该朝向哪个方向
        targetangle = Mathf.Atan2(movedir.x, movedir.z) * Mathf.Rad2Deg + thirdCam.eulerAngles.y;
        
        if (movedir.magnitude > 0.1f && !isAttack)
        {
            // 插值运算玩家的选择角度，使玩家朝向平滑靠近目标朝向
            float RotAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, smoothrottime);
            transform.rotation = Quaternion.Euler(0f, RotAngle, 0f);
            // 计算玩家移动朝向
            forwarddir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            ismove = true;
        }
        else
        {
            ismove = false;
        }

    } 
}

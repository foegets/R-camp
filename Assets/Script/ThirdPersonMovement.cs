using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController playercontroller;
    // 获得目标摄像机
    public Transform thirdcam;
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
    // 判断是否跳跃
    bool isjumping;
    //跳跃速度
    public float jumpspeed = 10.0f;
    //跳跃时间
    public float jumpTime = 0.5f;
    //累计跳跃时间用来判断是否结束跳跃
    public float jumpTimeFlag = 0;
    // 重力力度
    public float gravity = -10.0f;
    // 判断是否暂停
    public bool isPause;

    void Start()
    {
        isjumping = false;
        playercontroller = GetComponent<CharacterController>();
        // 锁定并隐藏光标
        Cursor.lockState = CursorLockMode.Locked;
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isPause)
            {
                // 按下ESC后给鼠标解锁
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0.001f;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isjumping)
        {
            isjumping = true;
        }

        if (isjumping)
        {
            if (jumpTimeFlag < jumpTime)
            {
                playercontroller.Move(transform.up * jumpspeed * Time.deltaTime);
                jumpTimeFlag += Time.deltaTime;
            }
            else if (jumpTime < jumpTimeFlag)
            {
                playercontroller.Move(transform.up * gravity * Time.deltaTime);
            }
            if (playercontroller.collisionFlags == CollisionFlags.Below)
            {
                jumpTimeFlag = 0;
                isjumping = false;
            }
        }
        else
        {
            if (playercontroller.collisionFlags != CollisionFlags.Below)
            {
                playercontroller.Move(transform.up * gravity * Time.deltaTime);
            }
        }

        movedir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        targetangle = Mathf.Atan2(movedir.x, movedir.z) * Mathf.Rad2Deg + thirdcam.eulerAngles.y; 
        float finalangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, smoothrottime);
        transform.rotation = Quaternion.Euler(0f, finalangle, 0f);
        if (movedir.magnitude > 0.1f)
        {
            Vector3 forwarddir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playercontroller.Move(forwarddir.normalized * movespeed * 2 * Time.deltaTime);
            }
            else
            {
                playercontroller.Move(forwarddir.normalized * movespeed * Time.deltaTime);
            }
            //playercontroller.Move(forwarddir.normalized * movespeed * Time.deltaTime);
        }

    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ThirdPersonMovement : MonoBehaviour
{
    //CharacterController playercontroller;
    // ���Ŀ�������
    public Transform thirdCam;
    // �����ƶ��ٶ�
    public float movespeed = 20.0f;
    // �ƶ�����
    Vector3 movedir;
    // ������ת�Ƕ�
    float targetangle;
    // ƽ����ת����ϵ��
    public float smoothrottime = 0.1f;
    // û����������ô�qwq
    float turnSmoothVelocity;
    //// �ж��Ƿ���Ծ
    //bool isjumping;
    ////��Ծ�ٶ�
    //public float jumpspeed = 10.0f;
    ////��Ծʱ��
    //public float jumpTime = 0.5f;
    ////�ۼ���Ծʱ�������ж��Ƿ������Ծ
    //public float jumpTimeFlag = 0;
    //// ��������
    //public float gravity = -10.0f;
    // �ж��Ƿ����ƶ�
    public bool ismove;
    // ��ҳ���
    Vector3 forwarddir;
    // �ж�����Ƿ��ڽ��й���
    public bool isAttack;
    void Start()
    {
        //isjumping = false;
        //playercontroller = GetComponent<CharacterController>();
        // ���������ع��
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
        // ��ȡ�������
        movedir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // �������Ӧ�ó����ĸ�����
        targetangle = Mathf.Atan2(movedir.x, movedir.z) * Mathf.Rad2Deg + thirdCam.eulerAngles.y;
        
        if (movedir.magnitude > 0.1f && !isAttack)
        {
            // ��ֵ������ҵ�ѡ��Ƕȣ�ʹ��ҳ���ƽ������Ŀ�곯��
            float RotAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, smoothrottime);
            transform.rotation = Quaternion.Euler(0f, RotAngle, 0f);
            // ��������ƶ�����
            forwarddir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            ismove = true;
        }
        else
        {
            ismove = false;
        }

    } 
}

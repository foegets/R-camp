using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController playercontroller;
    // ���Ŀ�������
    public Transform thirdcam;
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
    // �ж��Ƿ���Ծ
    bool isjumping;
    //��Ծ�ٶ�
    public float jumpspeed = 10.0f;
    //��Ծʱ��
    public float jumpTime = 0.5f;
    //�ۼ���Ծʱ�������ж��Ƿ������Ծ
    public float jumpTimeFlag = 0;
    // ��������
    public float gravity = -10.0f;
    // �ж��Ƿ���ͣ
    public bool isPause;

    void Start()
    {
        isjumping = false;
        playercontroller = GetComponent<CharacterController>();
        // ���������ع��
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
                // ����ESC���������
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

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class Playercontrol : MonoBehaviour
{
    // Start is called before the first frame update
    private int limitcoin;
    private coinUI coin;
    private coinUI coinUI;
    private Collider2D coll;
    private playanimition playanimition;
    public GameObject dropcoin;
    int limit;
    public PlayerInputControl inputcontrol;
    private Rigidbody2D rb;
    public Vector2 inputdirection;
    private physicalcheck physicalcheck1;
    [Header("基本参数")]
    public float Speed,Fast=0;
    public float JumpForce;
    public float hurtforce;
    public bool isHurt;
    public bool isdead;
    public bool isattack;
    [Header("物理材质")]
    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D isattack1;


    // Update is called once per frame
    private void Awake()
    {
        physicalcheck1=GetComponent<physicalcheck>();//获得文件内的公开变量
        rb = GetComponent<Rigidbody2D>();
        inputcontrol=new PlayerInputControl();
        inputcontrol.GamePlay.Jump.performed += Jump;
        inputcontrol.GamePlay.spacial.performed += Spacial;
        inputcontrol.GamePlay.attack.started += Playerattack;
        playanimition = GetComponent<playanimition>();
        coll= GetComponent<Collider2D>();
        coin = GetComponent<coinUI>();
        limitcoin = 1;
    }


    private void OnEnable()
    {
        inputcontrol.Enable();
    }
    private void OnDisable()
    {
        inputcontrol.Disable();
    }
    private void Update()
    {
        inputdirection = inputcontrol.GamePlay.Move.ReadValue<Vector2>();
        returnlocation();
      

    }
    private void FixedUpdate()
    {
        returnlocation();
        if(!isHurt&&!isattack)
        Move();
        CheckState();
  

    }
  
    private void returnlocation()
    {
        if (isdead==false)
        {
            if (rb.velocity.y <= -100)
                rb.velocity = new Vector2(0, (float)0.0664);
        }
    }

    public void Move()
    {
        if(Fast==0)
        rb.velocity = new Vector2(inputdirection.x * 1.5f*Speed * Time.deltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector2(inputdirection.x * Speed * Time.deltaTime*2, rb.velocity.y);
        int faceDir = (int)transform.localScale.x;
        if (inputdirection.x > 0)
            faceDir = 2;
        if (inputdirection.x < 0)
            faceDir = -2;
        transform.localScale = new Vector3(faceDir, 2, 2);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (physicalcheck1.isGround)
             limit = 1;
        if (physicalcheck1.isGround || limit >= 0)
        {
            rb.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            limit--;
        }
       
    }
    private void Playerattack(InputAction.CallbackContext context)
    {
        if (isHurt)
            return;
  
        playanimition.PlayAttack();
        isattack = true;
       


    }
    private void Spacial(InputAction.CallbackContext context)
    {
        if (Fast == 0)
        { Fast = 1;
          
        }
        else
        {
            Fast = 0;
           
        }
    }
    public void GetHurt(Transform attacker)
    {
        isHurt= true;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir*hurtforce, ForceMode2D.Impulse);
        rb.AddForce(transform.up *(hurtforce/5), ForceMode2D.Impulse);
        if (coinUI.CurrentCoinQuantity>=3&&isdead==false)
        {
            coinUI.CurrentCoinQuantity -= 3;
            for(int i=0;i<3;i++)
            {

                Instantiate(dropcoin,transform.position,Quaternion.identity);
            }
        }
    }
    public void PlayerDead()
    {
        if (limitcoin == 1)
        {
            isdead = true;
            inputcontrol.GamePlay.Disable();

            for (int i = 1; i <= 25; i++)
            {
                Instantiate(dropcoin, transform.position, Quaternion.identity);

            }
            limitcoin -= 1;
        }
    }
    private void CheckState()
    {
        if (!isattack)
            coll.sharedMaterial = physicalcheck1.isGround ? normal : wall;
        else
            coll.sharedMaterial = isattack1;
    }
    public void dragonspeed()
    {
        Speed = 350;
    }
}



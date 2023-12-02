using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class controlP : MonoBehaviour
{
    [Header("速度相关")]
    public float speed;
    public float jumpspeed;
    [Header("判断相关")]
    public bool isGround;
    public bool isJump;
    public bool pressedJump;
    public bool isAttack;
    public bool pressedAttack;
    [Header("组件相关")]
    public Transform foot;
    public LayerMask Ground;
    public Rigidbody2D rbody;
    public Collider2D Coll;
    public Animator playerAnim;
    public BoxCollider2D attackColl;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();
        playerAnim = GetComponent<Animator>();
        attackColl = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdataCheck();
        PlayerCrouch();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        FixedUpdateCheck();

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
       if(pressedJump)
        {
            pressedJump = false;
            rbody.velocity = new Vector2(rbody.velocity.x, jumpspeed);
            playerAnim.SetBool("playerjump", true);
        }
        if (isGround)
        {
            playerAnim.SetBool("playerjump", false);
        }
    }
    void PlayerCrouch()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            playerAnim.SetTrigger("attack");
        }
    }
    void FixedUpdateCheck()
    {
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, Ground);
    }
    void UpdataCheck()
    {
        if (Input.GetButtonDown("Jump" ) && isGround)
        {
            pressedJump = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("trap") || collision.CompareTag("enemy"))
        {

            Destroy(gameObject);

        }
    }

}

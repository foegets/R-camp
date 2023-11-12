using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public float playerMoveSpeed;
    public float playerJumpSpeed;
    public bool isGround;
    public LayerMask Ground;
    public Rigidbody2D playerRB;
    public Collider2D playerColl;
    public Animator playeranim;
    private float playerJunpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerColl = GetComponent<Collider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        playeranim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
    }
    void PlayerMove()
    {
        float horizontalNum = Input.GetAxis("Horizontal");
        float faceNum = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(playerMoveSpeed * horizontalNum, playerRB.velocity.y);
        playeranim.SetFloat("run", Mathf.Abs(playerMoveSpeed * horizontalNum));
        if (faceNum != 0)
        {
            //人物反转
            transform.localScale = new Vector3(-faceNum, transform.localScale.y, transform.localScale.z);
        }
    }
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerJumpSpeed);
        }
    }
  
}

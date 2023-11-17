using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnimator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private void Awake()
    {
        playerAnimator.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAnim();
    }
    public void SetAnim()//设置状态值
    {
        playerAnimator.SetFloat("Run", Mathf.Abs(rb.velocity.x));
        playerAnimator.SetFloat("airSpeed", rb.velocity.y);
        playerAnimator.SetBool("isGround", physicsCheck.isGround);
    }
}

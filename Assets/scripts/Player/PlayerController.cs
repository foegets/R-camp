using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PhysicsCheck physicsCheck;
    private Rigidbody2D rb;
    private BombTrap bombTrap;
    private TrailRenderer tr;
    private Vector2 mousePos;
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    public Transform tf;
    

    [Header("基本参数")]
    public float speed;
    private float originalGravity;
    
    [Header("跳跃参数")]
    public int jumpable = 2;
    public float jumpForce;
    private int jump;
    
    [Header("冲刺参数")]
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    public float dashingCoolTime;
    private float dashingStop = 0f;
    public bool canDash = true;
    public bool isDashing;

    

    // Start is called before the first frame update
    private void Awake() {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        bombTrap = GetComponent<BombTrap>();
        tr = GetComponent<TrailRenderer>();

        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Dash.started += Dash;
        originalGravity = rb.gravityScale;
    }
    private void OnEnable() {
        inputControl.Enable();    
    }
    private void OnDisable() {
        inputControl.Disable();    
    }
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate() {
        if(isDashing){                  //冲刺结束判定 不知道怎么写等待
            if(Time.time > dashingStop){
                tr.emitting = false;
                rb.gravityScale = originalGravity;
                isDashing = false;
            }
        }
        else{
        Move();

        }
    }
    public void Move(){
        rb.velocity = new Vector2(inputDirection.x*speed,rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if(inputDirection.x>0)
            faceDir = 1;
        if(inputDirection.x<0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir,1,1);
    }
    private void Jump(InputAction.CallbackContext obj){
        if(physicsCheck.isGround)
            jump = jumpable;
        if(jump > 0){
            if(rb.velocity.y < 0)
            {
                rb.velocity /= 3;
            }
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
            jump -= 1;
        }
    }
    private void Dash(InputAction.CallbackContext obj){
        if(Time.time > dashingCooldown){
        canDash = false;
        isDashing = true; 
        tr.emitting = true;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower,0f); //冲刺
        dashingCooldown = Time.time + dashingTime + dashingCoolTime;
        dashingStop = Time.time + dashingTime;
        }

        
    }

}
        
        
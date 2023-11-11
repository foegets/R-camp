using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //public float speedX = 5f;
    //public float speedY = 5f;
    private PhysicsCheck physicsCheck;
    private Rigidbody2D rb;
    private BombTrap bombTrap;
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;

    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public int jump = 2;
    // Start is called before the first frame update
    private void Awake() {
        inputControl = new PlayerInputControl();
        rb =GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        bombTrap = GetComponent<BombTrap>();

        inputControl.GamePlay.Jump.started += Jump;
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
        //float moveX = Input.GetAxis("Horizontal");//控制水平移动 A：-1 D：1
        //float moveY = Input.GetAxis("Vertical");//垂直方向
        //Vector2 position = transform.position;
        //position.x += moveX*speedX*Time.deltaTime; 
        //position.y += moveY*speedY*Time.deltaTime;
        //transform.position =position;
    }
    private void FixedUpdate() {
        move();
        touch();
    }
    public void move(){
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if(inputDirection.x>0)
            faceDir = 2;
        if(inputDirection.x<0)
            faceDir = -2;
        transform.localScale = new Vector3(faceDir,2,1);
    }
    private void Jump(InputAction.CallbackContext obj){
        //Debug.Log("JUMP");
        if(physicsCheck.isGround)
            jump=2;
        if(jump > 0){
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
            jump -= 1;
        }
    }
    public void touch(){   
        if(bombTrap.isTouch)
        {
            rb.AddForce(transform.up*30,ForceMode2D.Impulse);
        }
    }
}
        
        
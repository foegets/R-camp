using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class control : MonoBehaviour
{
    public Vector2 inputDirection;
    public float speed;
    public float jumpForce;
    public float checkRaduis;
    public bool isGround;
    public Rigidbody2D rb;
    public PlayerControl playerControl;
    public LayerMask groundLayer;
    private void Awake()
    {
        playerControl=new PlayerControl();
        playerControl.Player.Jump.started += Jump;
    }
    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = playerControl.Player.Move.ReadValue<Vector2>();
        Check();
    }
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        rb.velocity =
        new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if (isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void Check()
    {
        Vector3 vector3 =new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
        isGround=Physics2D.OverlapCircle(transform.position,checkRaduis,groundLayer);
    }
}

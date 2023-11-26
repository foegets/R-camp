using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("监听事件")]
    public SceneLoadEventSO loadEvent;
    public VoidEventSO afterSceneLoadedEvent;
    public int PlayerCoins { get; private set; } = 0;
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    private PlayerAnimation playerAnimation;
    public GameObject myBag;
    bool isOpen;
    [Header("基本参数")]
    public float speed=310f;
    public float jumpForce=16.5f;
    [Header("状态")]
    public bool isAttack;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        playerAnimation = GetComponent<PlayerAnimation>();
        //跳跃
        inputControl.Gameplay.Jump.started += Jump;
        //攻击
        inputControl.Gameplay.Attack.started += PlayerAttack;

    }
    private void OnEnable()
    {
        inputControl.Enable();
        loadEvent.LoadRequestEvent += OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        inputControl.Disable();
        loadEvent.LoadRequestEvent -= OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }

    

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        OpenMyBag(); 
    }
    private void FixedUpdate()
    {
        Move();
    }
    //场景加载过程停止控制
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.Gameplay.Disable();
    }
    //加载结束之后启动控制
    private void OnAfterSceneLoadedEvent()
    {
        inputControl.Gameplay.Enable();
    }
    public void Move()
    {
        //人物移动
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        //人物翻转
        int faceDir=(int)transform.localScale.x;
        if (inputDirection.x > 0) faceDir = 1;
        if (inputDirection.x < 0) faceDir = -1;
        transform.localScale=new Vector3(faceDir,1,1);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        playerAnimation.PlayAttack();
        isAttack = true;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Coins")) // 如果碰撞的游戏对象tag为"Coins"
        {
            PlayerCoins += 100; // 玩家金币数加100
            Debug.Log("Player coins: " + PlayerCoins); // 打印或显示玩家的金币数
        }
    }
    void OpenMyBag()
    {
        isOpen = myBag.activeSelf;
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
        }
    }

}

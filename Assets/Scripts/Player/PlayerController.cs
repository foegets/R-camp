using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("�����¼�")]
    public SceneLoadEventSO loadEvent;
    public VoidEventSO afterSceneLoadedEvent;

    public int PlayerCoins { get; private set; } = 0;
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private CapsuleCollider2D coll;
    public Vector2 inputDirection;
    private PlayerAnimation playerAnimation;
    public GameObject myBag;
    bool isOpen;
    [Header("��������")]
    public float speed=310f;
    public float jumpForce=16.5f;
    public float hurtForce;
    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    [Header("״̬")]
    public bool isAttack;
    public bool isHurt;
    public bool isDead;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        //��Ծ
        inputControl.Gameplay.Jump.started += Jump;
        //����
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
        CheckState();
        OpenMyBag(); 
    }
    private void FixedUpdate()
    {   if(!isHurt) Move();
    }
    //�������ع���ֹͣ����
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.Gameplay.Disable();
    }
    //���ؽ���֮����������
    private void OnAfterSceneLoadedEvent()
    {
        inputControl.Gameplay.Enable();
    }
    public void Move()
    {
        //�����ƶ�
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        //���﷭ת
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
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        playerAnimation.PlayAttack();
        isAttack = true;
        
    }
    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Blood")) // �����ײ����Ϸ����tagΪ"Blood"
    //    {
    //        character.nowHealth += 1f;
    //        float persentage = character.nowHealth / character.maxHealth;
    //        playerStatBar.OnHealthChange(persentage);//Ŀǰ����һ��bug

    //    }
    //}
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

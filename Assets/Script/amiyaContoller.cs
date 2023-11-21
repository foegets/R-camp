using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amiyaContoller : MonoBehaviour
{
    //unity���
    private Rigidbody2D rig;
    private BoxCollider2D box;
    private Animator ani;
    private CapsuleCollider2D capbox;
    

    [Header("�ƶ�����")]
    public float moveSpeed;
    private bool isRun;

    [Header("��Ծ����")]
    public bool isOnground;
    public float jumpSpeed;

    private int recordCount = 0;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        capbox = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        IsOnground();
        Jump();
        Attack();
    }
    
    void Filp()
    {
        if (isRun)
        {
            if(rig.velocity.x>0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if(rig.velocity.x<-0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void IsOnground()
    {
        isOnground = capbox.IsTouchingLayers(LayerMask.GetMask("platform"));
    }
    void Run()
    {
         float speed = Input.GetAxis("Horizontal")*moveSpeed;
         rig.velocity = new Vector2(speed, rig.velocity.y);
         isRun = Mathf.Abs(rig.velocity.x) > 0.1f; 
         Filp();
         ani.SetBool("isRun", isRun&&isOnground);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump")&&isOnground)
        {
            Vector2 jumpVel = new(0.0f, jumpSpeed);
            rig.velocity = Vector2.up * jumpVel;
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Attack"))
        {
            ani.SetTrigger("isAttack");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("record") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D") ;
        {
            Debug.Log(other.GetType().ToString());
            recordCount++;
            Destroy(other.gameObject); 
        }
    }
}

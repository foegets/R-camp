using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class amiyaContoller : MonoBehaviour
{
    //unity组件
    private Rigidbody2D rig;
    private BoxCollider2D box;
    private Animator ani;
    private CapsuleCollider2D capbox;
    public Text recordText;
    

    [Header("移动参数")]
    public float moveSpeed;
    private bool isRun;

    [Header("跳跃参数")]
    public bool isOnground;
    public float jumpSpeed;

    [Header("数值参数")]
    public bool isDead;
    public int blood;
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

    void Hurt()
    {
        
    }

    void Dead()
    {
        //gameManager.GameOver(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag.ToString()+" "+ other.GetType().ToString());

        if (other.CompareTag("record"))
        {
            Debug.Log(other.GetType().ToString());
            recordCount++;
            recordText.text = recordCount.ToString();
            Destroy(other.gameObject); 
        }
    }
}

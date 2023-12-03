using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基本参数")]
    public float walkSpeed;

    public float runSpeed;

    public float faceDir;

    public float standbyTime;

    public bool checkIsPlayerRange;

    public float enemyScore;

    public Vector2 standbyPoint;
    public float standbyPointRadius;

    [Header("状态")]
    public bool isWalk;

    public bool isRun;

    public bool isPlayerinRange;

    public bool isInLeftStandbyCheckPoint;
    public bool isInRightStandbyCheckPoint;
    public bool isStandbyPoint;

    [Header("检测参数")]
    public Vector2 pointA;
    public Vector2 pointB;
    
    

    public BaseState currentState;
    public BaseState state_a;
    public BaseState state_b;
    public BaseState state_c;
    public BaseState deadState;

    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D collider2d;
    public LayerMask mask;
    public LayerMask standbyPointCheckMask;
    public Collider2D checkPlayer;
    public Character character;

    private int a = 1;
    private int b = 1;


    // Start is called before the first frame update
    private void OnEnable()
    {
        currentState = state_a;
        currentState.OnEnter(this);
    }
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        character = GetComponent<Character>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Animator();
        GetFaceDir();
        FlipPointAB();
        CheckisPlayerinRange();
        CheckisStandbypoint();
        currentState.LogicUpdate();
        checkPlayer = CheckisPlayerinRange();

        if(character.isdead&&b==1)
        {
            b++;
            Debug.Log("ToDeadState");
            currentState = deadState;
            currentState.OnEnter(this);
        }
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }
    #region CHECK FACTION
    public virtual void GetFaceDir()
    {
        faceDir = transform.localScale.x;
    }

    protected virtual Collider2D CheckisPlayerinRange()
    {
        Collider2D player;
   
        isPlayerinRange = Physics2D.OverlapArea((Vector2)transform.position+pointA, (Vector2)transform.position + pointB,mask);
        if (isPlayerinRange)
        {
            player =  Physics2D.OverlapArea((Vector2)transform.position + pointA, (Vector2)transform.position + pointB, mask);
            return player;
        }
        
        else return null;
    }

    private void FlipPointAB()
    {
        if(faceDir<0&&a==1)
        {
            pointA.x *= -1;
            pointB.x *= -1;
            a *=-1;
        }
        if(faceDir>0&&a==-1)
        {
            pointA.x *= -1;
            pointB.x *= -1;
            a *= -1;
        }
       
            
    }
    private void CheckisStandbypoint()
    {
        if (currentState == state_b && isPlayerinRange == false)
        {
            isStandbyPoint = Physics2D.OverlapCircle(standbyPoint, standbyPointRadius, standbyPointCheckMask);
        }
        
    }

    #endregion

    private void Animator()
    {
        anim.SetBool("isRun", isRun);
    }

    public void HurtAnimation()
    {
        anim.SetTrigger("Hurt");
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "CheckLeftStandbyPoint" && currentState == state_a)
        {
            isInLeftStandbyCheckPoint = true;
        }
        if (collision.name == "CheckRightStandbyPoint" && currentState == state_a)
        {
            isInRightStandbyCheckPoint = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CheckMachine")
        {
            isInLeftStandbyCheckPoint= false;
            isInRightStandbyCheckPoint= false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 A = (Vector2)transform.position + pointA;
        Vector3 B = (Vector2)transform.position + pointB;
        Vector3 position;
        Vector3 size;
        position = new Vector3((A.x+B.x)/2,(A.y + B.y) /2,0);
        size = new Vector3(Mathf.Abs(A.x-B.x), Mathf.Abs(A.y - B.y), 0.15f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(position,size);
        Gizmos.DrawWireSphere(standbyPoint, standbyPointRadius);
    }
}

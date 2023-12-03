using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{/*�ʼǣ�private���͵ı���ֻ���ڵ�ǰ�ࣨclass����ʹ�ã���public�����������class�б����ã��ҿ�����Inspect�����п���*/
    public UnityEvent OnDie;

    public PlayerInputControl inputControl;

    private Rigidbody2D rb;//rb�Ǹ��������

    private PhysicsCheck physicsCheck;//private���͵ı���ֻ����������б�����

    private Character character;

    [Header("������ƶ�����")]
    public Vector2 inputDirection;//�����洢Vector2���͵ı���,�����ڴ�������ķ���

    [Header("��������")]
    public float speed;//�ٶȵ�ֵ

    public float jumpForce;//��Ծ����

    public float exitTime = 300;

    public bool isHurt;//�ж��Ƿ�����

    public bool isDead;//�ж��Ƿ�������״̬

    public float hurtForce;//����ʱ�ܵ��ĳ����

    private void Awake(){
        rb=GetComponent<Rigidbody2D>();//�����ĸ�ֵ
        physicsCheck=GetComponent<PhysicsCheck>();
        character=GetComponent<Character>();

        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Jump.started +=/*+=����ע��һ������*/ Jump;//��Ծ���
    }



    private void OnEnable(){//ִ�д˺���ʱʹinputControl��������
        inputControl.Enable();
    }

    private void OnDisable()
    {//ִ�д˺���ʱʹinputControl��������
        inputControl.Disable();

    }
    public void ExitTimeCounter()
    {
        
        for (; exitTime >= 0;)
        {
            exitTime-=Time.deltaTime;
        }
        
            ExitGame();
        
    }
    private void Update(){
        inputDirection/*���ڴ�������ķ���*/= inputControl.Gameplay.Move/*GamePlay���Actions*/.ReadValue/*��ȡ��ֵ*/<Vector2/*����ȡ��ֵ������*/>();
        //����������������ƶ�����
        if (character.currentHealth <= 0)
        {
            rb.velocity=new Vector2(0,0);
            OnDie?.Invoke();/*���������ĺ���*/


        }
        if(transform.position.y < -11.5) {
            rb.velocity = new Vector2(0, 0);
            OnDie?.Invoke();/*���������ĺ���*/
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void FixedUpdate(){//�������йصĶ�����FixedUpdateִ��
        if (!isHurt)//���û�д�������״̬
        Move();//�����ƶ�
    }

    public void Move()
    {
        //�����ƶ�
        rb.velocity=new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);

        //ʵ������ķ�ת��ת��
        int faceDir=(int)transform.localScale.x;
        if(inputDirection.x>0)faceDir=1;
        else if(inputDirection.x<0)faceDir=-1;
        

        //���﷭ת
        transform.localScale =new Vector3(faceDir,1,1);
    }


    private void Jump (InputAction. CallbackContext obj){
        //Debug.Log("JUMP");
        if(physicsCheck.isGround)
        rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);//��Ծ��ʩ��һ�����ϵ�������
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity=Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);//ˮƽ�����ɵ���
        rb.AddForce(transform.up * hurtForce*0.7f, ForceMode2D.Impulse);//��ֱ���򱻻��ɵ���
        //transform.up��ôʹ��ͻȻ���ˣ��ǵõ���Ծ��ʵ�����ٿ�������
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();//ʹ���������沿�ֵĲٿر��ر�
    }
}

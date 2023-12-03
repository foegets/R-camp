using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
	private CapsuleCollider2D capsuleCollider;
	[Header("������")]

	public bool manual;
	public Vector2 bottomOffset;

	public Vector2 leftOffset;
	public Vector2 rightOffset;
	
	public float checkRaduis;

	public LayerMask groundLayer;
	[Header("�ж�״̬")]
	public bool isGround;
	public bool touchLeftWall;//��ײ��ǽ
	public bool touchRightWall;//��ײ��ǽ

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();

		if(!manual)
		{
			rightOffset = new Vector2((capsuleCollider.bounds.size.x/2 + capsuleCollider.offset.x),capsuleCollider.bounds.size.y/2);
			leftOffset =new Vector2(-rightOffset.x,rightOffset.y);
		}
    }

    private void Update(){
		Check();
	}

	public void Check(){
		//������
		isGround=Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

		//�ж��Ƿ�ײǽ
		touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis,groundLayer);
		touchRightWall= Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
    }

	private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}

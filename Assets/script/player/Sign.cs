using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//�ýű������������ֱ�ʶ
public class Sign : MonoBehaviour
{
    public GameObject signSprite;//�����object
    private bool canPress;//����trigger�иı�ı�boolֵ
    public Transform playerTrans;
    private GameInput gameInput;
    private IInteractable targetItem;
    
    private void Awake()
    {
        gameInput = new GameInput();
        gameInput.Enable();
    }
    private void OnEnable()
    {
        gameInput.Player.Confirm.started += OnConfirm;

    }



    private void Update()
    {
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;//ͨ��canpressȷ���Ƿ�������object��spriteRenderer
        signSprite.transform.localScale = playerTrans.localScale;//ʹ��ʶ������Ϊ�����ƶ���ת
    }


    
    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();//���°�����canPressΪtrueʱ�����ô������Ľӿڵķ������л���
        }
    }

    //����trigger������ȡ��trigger����������
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();//��ȡ������Я���Ľӿ�
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            canPress = false;
        }
    }
 
}

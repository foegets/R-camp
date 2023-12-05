using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//该脚本用来启动各种标识
public class Sign : MonoBehaviour
{
    public GameObject signSprite;//获得子object
    private bool canPress;//放在trigger中改变改变bool值
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
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;//通过canpress确定是否启动子object的spriteRenderer
        signSprite.transform.localScale = playerTrans.localScale;//使标识不会因为左右移动翻转
    }


    
    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();//按下按键且canPress为true时，启用触发器的接口的方法进行互动
        }
    }

    //其他trigger进入后读取该trigger属性做参数
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();//获取触发器携带的接口
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

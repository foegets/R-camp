using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    //创建输入系统，负责识别当前使用的是什么设备
    private PlayerInputControl playerInput;
    //可互动按钮动画的实现
    private Animator anim;
    //为了让按钮不翻转，获取一下player的transform，让按钮的transform和player一致
    public Transform playerTrans;
    //获取一下signSprite这个项目
    public GameObject signSprite;
    //获得这个可交互的物体捏（直接用接口来创建
    private IInteractable targetItem;
    //创建状态判断是否可以交互
    private bool canPress;

    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();
        //启动输入系统
        playerInput = new PlayerInputControl();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        /*InputSystem.onActionChange += OnActionChange;*/
        //当这个confirm被按下的时候执行这个函数
        playerInput.Gameplay.Confirm.started += OnConfirm;
    }

    private void OnDisable()
    {
        //“人物被关闭的时候，你也就被关闭罢！”
        canPress = false;
    }
    private void Update()
    {
        //如果是可互动的物体，就激活这个项目
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        //让按钮的transform和player一致
        signSprite.transform.localScale = playerTrans.localScale;
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if(canPress)
        {
            targetItem.TriggerAction();
            GetComponent<AudioDefination>()?.PlayAudioClip();
        }
    }

    /*private void OnActionChange(object obj,InputActionChange actionChange)
    {
        var d = ((InputAction)obj).activeControl.device;
        //切换设备的同时切换动画
        switch(d.device)
        {
            case Keyboard:
                anim.Play("keyboard");
                break;
            case DualShockGamepad:
                anim.Play("ps4");
                break;
        }
        
    }*/


    private void OnTriggerStay2D(Collider2D other)
    {   
        //同上
        if(other.CompareTag("interactable"))
        {
            canPress = true;
            anim.Play("keyboard");
            targetItem = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }
}

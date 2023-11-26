using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    //获取这个组件来控制播放什么图片
    private SpriteRenderer spriteRenderer;
    //
    public Sprite openSprite;
    public Sprite closeSprite;
    //确保宝箱开过了就不能再开了
    public bool isDone;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        //如果宝箱已经被打开就播放open的图片，否则播放close
        spriteRenderer.sprite = isDone?openSprite:closeSprite;
    }

    public void TriggerAction()
    {
        Debug.Log("Open Chest!");
        //如果宝箱没有被打开，就执行打开的函数方法
        if(!isDone)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        //打开宝箱，换图片，切换状态
        spriteRenderer.sprite = openSprite;
        isDone = true;
        this.gameObject.tag = "Untagged";
    }
}

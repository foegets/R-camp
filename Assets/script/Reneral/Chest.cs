using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    //该四行变量用于开启宝箱后切换图片
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;
    }
    private void OpenChest()
    {
        spriteRenderer.sprite = openSprite;

    }
    public void TriggerAction()
    {
        if (!isDone)
        {
            OpenChest();
            isDone = true;
        }
    }

}

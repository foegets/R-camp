using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    //�����б������ڿ���������л�ͼƬ
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

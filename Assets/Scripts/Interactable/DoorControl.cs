using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour, IInteractable
{
    public bool IsOpenable;
    public BoxCollider2D doorCollider;
    public GameObject door;
    public SpriteRenderer spriteRenderer;
    public Sprite DoorClose;
    public Sprite DoorOpen;

    public void TriggerAction()
    {
        Door();
    }

    private void Awake()
    {
        doorCollider = GetComponentInParent<BoxCollider2D>();
    }

    private void Door()
    {
        if(IsOpenable)
        {
            Destroy(door.GetComponent<BoxCollider2D>());
            Debug.Log("DoorOpen~");
            spriteRenderer.sprite = DoorOpen;
        }
        else
        {
            Debug.Log("You can't open the door from this side");
        }
    }
}

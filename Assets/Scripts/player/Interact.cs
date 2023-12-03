using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interact : MonoBehaviour
{
    private PlayerInputControl playerInput;
    private IInteractable targetItem;
    private bool canPress;
    private void Awake()
    {
       playerInput = new PlayerInputControl();
       playerInput.Enable();
       playerInput.Gameplay.Confirm.started += Confirm;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();
        }
    }

   

    private void Confirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            Debug.Log("YES!");
            targetItem.TriggerAction();
        }
     }



    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Interactable"))
    //    {
    //        collision.GetComponent<Interacting>().IsInteract = false;
    //    }
    //   
    //}

}

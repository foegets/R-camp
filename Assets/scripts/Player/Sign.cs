using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    public GameObject PressE;
    private Collider2D cd;
    public bool canPress;
    private IInteractable targetItem;
    private PlayerInputControl playerInput;

    private void Awake()
    {
        playerInput = new PlayerInputControl();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
        playerInput.GamePlay.Confirm.started += OnConfirm;
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();
        }
    }

    private void OnActionChange(object arg1, InputActionChange change)
    {
    }

    private void Update()
    {
        if (canPress)
        {
            PressE.SetActive(true);
        }
        else
        {
            PressE.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = other.GetComponent<IInteractable>();
        }
        else
        {
            canPress = false;
        }
    }
}

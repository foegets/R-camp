using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public enum InputMode
    {
        Event, InputMgr, InputSys
    }
    [SerializeField] InputMode inputMode = InputMode.Event;

    public Vector2 MovementInput { get; private set; }
    public delegate void OnJumpKeyPressed();

    public OnJumpKeyPressed DoJump;


    // Update is called once per frame
    void Update()
    {
        //直接读取状态
        HandleInputOnUpdate();
    }

    void HandleInputOnUpdate()
    {
        //Input Manager
        if (inputMode == InputMode.InputMgr && Input.GetKey(KeyCode.Space))
        {
            print("InputMgr: press [space]");
        }

        //Input System
        if (inputMode == InputMode.InputSys && Keyboard.current.spaceKey.isPressed)
   {
  print("InputSys: press [space]");
        }
    }


    #region InputSystem事件写法
    public void MoveCallBack(InputAction.CallbackContext context)
    {
        MovementInput = context.action.ReadValue<Vector2>();
        if (inputMode == InputMode.Event && context.action.name == "Move")
        {
            print(MovementInput);
        }
    }

    public void jumpCallBack(InputAction.CallbackContext context)
    {
        if (inputMode == InputMode.Event && context.action.name == "Jump")
        {
            print(context.action.IsPressed());

            if (context.action.IsPressed())
            {
                DoJump();
            }
        }

    }
    #endregion

}

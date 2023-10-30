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
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    void Start()
    {
        
    }

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
        if (inputMode == InputMode.Event && context.action.name == "Move")
        {
            print(context.action.ReadValue<Vector2>());
        }
    }

    public void jumpCallBack(InputAction.CallbackContext context)
    {
        if (inputMode == InputMode.Event && context.action.name == "Jump")
        {
            print(context.action.IsPressed());
        }
    }
    #endregion

}

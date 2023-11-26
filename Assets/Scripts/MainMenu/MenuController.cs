using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    private PlayerInputControl inputControl;
    public SceneLoadEventSO loadEventSO;

    public GameSceneSO sceneToGo;
    public Vector3 positionToGo;
    private GameObject canvas; 


    private void OnEnable()
    {
        inputControl.Enable();
       
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        inputControl.Gameplay.Confirm.started += Started;
        positionToGo = new Vector3(0,0,0);
        canvas = GameObject.Find("Canvas");
        canvas.gameObject.SetActive(false);
    }

    



    private void Started(InputAction.CallbackContext context)
    {
        canvas.gameObject.SetActive(true);
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, true);
    }
}

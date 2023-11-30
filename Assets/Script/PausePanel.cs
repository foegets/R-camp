using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : BasePanel
{
    
    void Start()
    {
        name = "PausePanel";
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            ClosePanel();
        }
    }

}

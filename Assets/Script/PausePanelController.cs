using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    public GameObject panel;
    public bool isopen;
    void Start()
    {
        Time.timeScale = 1f;
        isopen = false;
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isopen)
            {
                panel.SetActive(false);
                Time.timeScale = 1f;
                isopen = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                panel.SetActive(true);
                Time.timeScale = 0f;
                isopen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}

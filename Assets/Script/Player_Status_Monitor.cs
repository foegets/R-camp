using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Player_Status_Monitor : MonoBehaviour
{
    // ���ó�ʼѪ��
    public float HP = 100;  

    // ��ȡ����
    public Slider Player_HP;
    public Slider Player_Engry;
    public VideoPlayer videoplayer;
    //public GameObject PausePanel;
    // ��ǳ�ʼλ��
    Transform OriPos;
    // �ж��Ƿ��PausePanel����
    bool isOpen;
    void Start()
    {
        OriPos = transform;
        Player_HP.maxValue = HP;
        Player_HP.value = HP;
        Player_Engry.value = 0;
        Time.timeScale = 1f;
        videoplayer.Prepare();
        isOpen = false;
    }
  
    void Update()
    {
        if (HP < Player_HP.value)
        {
            Player_HP.value -= 0.5f;
        }
        if (transform.position.y < -10)
        {
            transform.position = OriPos.position;
            transform.rotation = OriPos.rotation;
        }
        if (Player_Engry.value == 5)
        {
            Player_HP.value += 10;
            videoplayer.Play();
            Player_Engry.value = 0;
        }
        if (HP <= 0)
        {
            UIManager.Instance.OpenPanel(UIConst.DeathPanel);
            gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isOpen)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                UIManager.Instance.OpenPanel(UIConst.PausePanel);
            }
            isOpen = !isOpen;
        }
    }
}

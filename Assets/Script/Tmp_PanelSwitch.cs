using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tmp_PanelSwitch : MonoBehaviour
{
    // 获取UI界面
    public GameObject panel;

    private void Update()
    {
        
    }

    public void Switch()
    {
        panel.SetActive(!panel.activeSelf);
    }
}

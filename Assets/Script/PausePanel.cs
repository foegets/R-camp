using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    Transform Guide;
    Transform Setting;
    Transform Package;
    void Start()
    {

        name = "PausePanel";
        Guide = transform.Find("option/GuideSwitch");
        Setting = transform.Find("option/SettingButton");
        Package = transform.Find("option/BackpackPanelSwitch");
        Guide.GetComponent<Button>().onClick.AddListener(OnClickGuide);
        Setting.GetComponent<Button>().onClick.AddListener(OnClickSetting);
        Package.GetComponent<Button>().onClick.AddListener(OnClickPackage);
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

    void OnClickGuide()
    {
        UIManager.Instance.OpenPanel(UIConst.PausePanel);
    }

    void OnClickSetting()
    {
        UIManager.Instance.OpenPanel(UIConst.SettingPanel);
    }

    void OnClickPackage()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }
}

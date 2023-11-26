using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    // 绑定背包界面的按钮及其它会变化的UI
    // 按钮
    public Transform ToWeapon;
    public Transform ToFood;
    public Transform CloseButton;
    public Transform ToRight;
    public Transform ToLeft;
    // 动态UI
    public Transform CurPackType;
    public Transform CurPackCapacity;

    void Start()
    {
        // 初始化按钮点击函数
        InitClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region 点击按钮相关
    private void InitClick()
    {
        CloseButton.GetComponent<Button>().onClick.AddListener(OnClickClose);
        ToWeapon.GetComponent<Button>().onClick.AddListener(OnClickToWeapon);
        ToFood.GetComponent<Button>().onClick.AddListener(OnClickToFood);
        ToRight.GetComponent<Button>().onClick.AddListener(OnClickToRight);
        ToLeft.GetComponent<Button>().onClick.AddListener(OnClickToLeft);
    }

    private void OnClickClose()
    {
        
    }
    private void OnClickToWeapon()
    {

    }
    private void OnClickToFood()
    {

    }
    private void OnClickToRight()
    {

    }
    private void OnClickToLeft()
    {

    }

    #endregion
}

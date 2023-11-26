using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    // �󶨱�������İ�ť��������仯��UI
    // ��ť
    public Transform ToWeapon;
    public Transform ToFood;
    public Transform CloseButton;
    public Transform ToRight;
    public Transform ToLeft;
    // ��̬UI
    public Transform CurPackType;
    public Transform CurPackCapacity;

    void Start()
    {
        // ��ʼ����ť�������
        InitClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region �����ť���
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

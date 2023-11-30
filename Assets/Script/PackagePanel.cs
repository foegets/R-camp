using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    // ��ȡ�����Ӷ���
    private Transform SelectMenus;
    private Transform SelectWeapon;
    private Transform SelectFood;
    private Transform TagName;
    private Transform CloseBin;
    private Transform Center;
    private Transform BagScroll;
    private Transform DetailPanel;
    private Transform ToRight;
    private Transform ToLeft;
    private Transform DelePanel;
    private Transform DeleBackBin;
    private Transform DeleNumTxt;
    private Transform DeleConfirmBin;
    private Transform ButtonMenus;
    private Transform DeleBin;
    private Transform DetailBin;
    // ��������Ԥ����
    public GameObject PackItemPrefab;
    // ���ѡ����Ʒ��uid
    private string uid;
    public string ItemUid
    {
        get
        {
            return uid; 
        }
        set
        {
            uid = value;
            RefreshDetailPanel();
        }
    }

    private void Awake()
    {
        name = "PackagePanel";
        InitBagUI();
        InitClickBin();
    }
    private void Start()
    {
        RefreshBagScroll();
    }

    void InitBagUI()
    {
        SelectMenus = transform.Find("CenterTop/SelectMenus");
        SelectWeapon = transform.Find("CenterTop/SelectMenus/Weapon");
        SelectFood = transform.Find("CenterTop/SelectMenus/Food");
        TagName = transform.Find("LeftTop/TagName");
        CloseBin = transform.Find("RightTop/Close");
        Center = transform.Find("Center");
        BagScroll = transform.Find("Center/Scroll View");
        DetailPanel = transform.Find("Center/DetailPanel");
        ToRight = transform.Find("Right/ToRight");
        ToLeft = transform.Find("Left/ToLeft");
        ButtonMenus = transform.Find("Button/ButtonMenu");
        DeleBin = transform.Find("Button/ButtonMenu/DeleBin");
        DetailBin = transform.Find("Button/ButtonMenu/DetailBin");
        DelePanel = transform.Find("Button/DelePanel");
        DeleBackBin = transform.Find("Button/DelePanel/Back");
        DeleNumTxt = transform.Find("Button/DelePanel/SelectNumber");
        DeleConfirmBin = transform.Find("Button/DelePanel/DeleConfirm");

        DelePanel.gameObject.SetActive(false);
        ButtonMenus.gameObject.SetActive(true);
    }

    void InitClickBin()
    {
        SelectWeapon.GetComponent<Button>().onClick.AddListener(OnClickWeapon);
        SelectFood.GetComponent<Button>().onClick.AddListener(OnClickFood);
        CloseBin.GetComponent<Button>().onClick.AddListener(OnClickClose);
        ToRight.GetComponent<Button>().onClick.AddListener(OnClickToRight);
        ToLeft.GetComponent<Button>().onClick.AddListener(OnClickToLeft);
        DeleBin.GetComponent<Button>().onClick.AddListener(OnClickDeleBin);
        DetailBin.GetComponent<Button>().onClick.AddListener(OnClickDetailBin);
        DeleBackBin.GetComponent<Button>().onClick.AddListener(OnClickDeleBack);
        DeleConfirmBin.GetComponent<Button>().onClick.AddListener(OnClickDeleConfirm);
    }

    // ˢ������������Ϣ
    void RefreshDetailPanel()
    {
        PackageLocalItem itemDynInfo = GameManager.Instance.GetPackLocalItem(uid);
        DetailPanel.GetComponent<PackageDetail>().Refresh(itemDynInfo, this);
    }

    // ˢ�±�����Ʒ��Ϣ
    void RefreshBagScroll()
    {
        // ��������ԭ���еĶ���
        RectTransform scrollContent = BagScroll.GetComponent<ScrollRect>().content;
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        // ��õ�ǰ��������
        List<PackageLocalItem> packLocalItems = GameManager.Instance.GetSortedPackLocalData();
        if (packLocalItems == null)
        {
            print("������̬�����б�Ϊ��");
        }
        foreach (PackageLocalItem item in packLocalItems)
        {
            if (item == null)
            {
                print("������̬����Ϊ��");
            }
            Transform PackUIItem = Instantiate(PackItemPrefab.transform, scrollContent) as Transform;
            PackageCell packCell = PackUIItem.GetComponent<PackageCell>();
            packCell.Refresh(item, this);
        }
    }
    void OnClickWeapon()
    {
        print("����ɹ�");
    }
    void OnClickFood()
    {
        print("����ɹ�");
    }
    void OnClickClose()
    {
        print("����ɹ�");
        ClosePanel();
    }
    void OnClickToRight()
    {
        print("����ɹ�");
    }
    void OnClickToLeft()
    {
        print("����ɹ�");
    }
    void OnClickDeleBin()
    {
        print("����ɹ�");
    }
    void OnClickDetailBin()
    {
        print("����ɹ�");
    }
    void OnClickDeleBack()
    {
        print("����ɹ�");
    }
    void OnClickDeleConfirm()
    {
        print("����ɹ�");
    }
}

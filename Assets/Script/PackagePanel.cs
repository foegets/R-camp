using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    // 获取背包子对象
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
    // 添加物体的预制体
    public GameObject PackItemPrefab;
    // 获得选中物品的uid
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

    // 刷新详情面板的信息
    void RefreshDetailPanel()
    {
        PackageLocalItem itemDynInfo = GameManager.Instance.GetPackLocalItem(uid);
        DetailPanel.GetComponent<PackageDetail>().Refresh(itemDynInfo, this);
    }

    // 刷新背包物品信息
    void RefreshBagScroll()
    {
        // 清理背包里原本有的东西
        RectTransform scrollContent = BagScroll.GetComponent<ScrollRect>().content;
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        // 获得当前背包数据
        List<PackageLocalItem> packLocalItems = GameManager.Instance.GetSortedPackLocalData();
        if (packLocalItems == null)
        {
            print("背包动态数据列表为空");
        }
        foreach (PackageLocalItem item in packLocalItems)
        {
            if (item == null)
            {
                print("背包动态数据为空");
            }
            Transform PackUIItem = Instantiate(PackItemPrefab.transform, scrollContent) as Transform;
            PackageCell packCell = PackUIItem.GetComponent<PackageCell>();
            packCell.Refresh(item, this);
        }
    }
    void OnClickWeapon()
    {
        print("点击成功");
    }
    void OnClickFood()
    {
        print("点击成功");
    }
    void OnClickClose()
    {
        print("点击成功");
        ClosePanel();
    }
    void OnClickToRight()
    {
        print("点击成功");
    }
    void OnClickToLeft()
    {
        print("点击成功");
    }
    void OnClickDeleBin()
    {
        print("点击成功");
    }
    void OnClickDetailBin()
    {
        print("点击成功");
    }
    void OnClickDeleBack()
    {
        print("点击成功");
    }
    void OnClickDeleConfirm()
    {
        print("点击成功");
    }
}

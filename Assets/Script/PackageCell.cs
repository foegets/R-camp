using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PackageCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform ItemIcon;
    private Transform ItemHead;
    private Transform ItemisNew;
    private Transform ItemLevel;
    private Transform ItemStars;
    private Transform ItemSelect;
    private Transform ItemDeleSelect;
    private Transform SelectAni;
    private Transform MouseOverAni;

    private PackageLocalItem DynamicData;
    private PackageTabItem StaticData;
    private PackagePanel PackUI;

    private void Awake()
    {
        InitItem();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void InitItem()
    {
        ItemIcon = transform.Find("Top/Icon");
        ItemHead = transform.Find("Top/Head");
        ItemisNew = transform.Find("Top/New");
        ItemLevel = transform.Find("Button/Level");
        ItemStars = transform.Find("Button/Stars");
        ItemSelect = transform.Find("Select");
        ItemDeleSelect = transform.Find("DeleSelect");
        SelectAni = transform.Find("SelectAni");
        MouseOverAni = transform.Find("MouseOverAni");

        ItemSelect.gameObject.SetActive(false);
        SelectAni.gameObject.SetActive(false);
        MouseOverAni.gameObject.SetActive(false);
    }

    // 刷新物体信息
    public void Refresh(PackageLocalItem LocalData, PackagePanel uiPack)
    {
        // 获取物品的动态数据
        DynamicData = LocalData;
        if (DynamicData == null)
        {
            print("动态数据为空");
        }
        // 获得物品的静态数据
        PackageTabItem TableData = GameManager.Instance.GetPackTabItem(DynamicData.id);
        StaticData = TableData;
        if (StaticData == null)
        {
            print("静态数据为空");
        }
        PackUI = uiPack;
        // 更新物品等级
        if (ItemLevel.GetComponent<TextMeshProUGUI>() == null)
        {
            print("找不到Text组件");
        }
        ItemLevel.GetComponent<TextMeshProUGUI>().text = "Lv." + DynamicData.level;
        if (ItemLevel == null)
        {
            print("Level为空");
        }
        // 更新物品是否为新物品
        ItemisNew.gameObject.SetActive(DynamicData.isnew);
        if (ItemisNew.gameObject == null)
        {
            print("ItemisNew为空");
        }
        // 更新物品图片
        Texture2D icon = (Texture2D)Resources.Load<Texture2D>(this.StaticData.iconPath);
        Sprite temp = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), new Vector2(0, 0));
        ItemIcon.GetComponent<Image>().sprite = temp;
        // 更新物品星级
        refreshStars();
    }
    // 星级刷新函数
    public void refreshStars()
    {
        for(int i = 0;i < ItemStars.childCount;i++)
        {
            Transform Star = ItemStars.GetChild(i);
            if (this.StaticData.star > i)
            {
                Star.gameObject.SetActive(true);
            }
            else
            {
                Star.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.DynamicData.uid == PackUI.ItemUid)
        {
            return;
        }
        else
        {
            PackUI.ItemUid = DynamicData.uid;
        }
        SelectAni.gameObject.SetActive(true);
        SelectAni.GetComponent<Animator>().SetTrigger("Select");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOverAni.gameObject.SetActive(true);
        MouseOverAni.GetComponent<Animator>().SetTrigger("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOverAni.GetComponent<Animator>().SetTrigger("Exit");
    }
}

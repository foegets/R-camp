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

    // ˢ��������Ϣ
    public void Refresh(PackageLocalItem LocalData, PackagePanel uiPack)
    {
        // ��ȡ��Ʒ�Ķ�̬����
        DynamicData = LocalData;
        if (DynamicData == null)
        {
            print("��̬����Ϊ��");
        }
        // �����Ʒ�ľ�̬����
        PackageTabItem TableData = GameManager.Instance.GetPackTabItem(DynamicData.id);
        StaticData = TableData;
        if (StaticData == null)
        {
            print("��̬����Ϊ��");
        }
        PackUI = uiPack;
        // ������Ʒ�ȼ�
        if (ItemLevel.GetComponent<TextMeshProUGUI>() == null)
        {
            print("�Ҳ���Text���");
        }
        ItemLevel.GetComponent<TextMeshProUGUI>().text = "Lv." + DynamicData.level;
        if (ItemLevel == null)
        {
            print("LevelΪ��");
        }
        // ������Ʒ�Ƿ�Ϊ����Ʒ
        ItemisNew.gameObject.SetActive(DynamicData.isnew);
        if (ItemisNew.gameObject == null)
        {
            print("ItemisNewΪ��");
        }
        // ������ƷͼƬ
        Texture2D icon = (Texture2D)Resources.Load<Texture2D>(this.StaticData.iconPath);
        Sprite temp = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), new Vector2(0, 0));
        ItemIcon.GetComponent<Image>().sprite = temp;
        // ������Ʒ�Ǽ�
        refreshStars();
    }
    // �Ǽ�ˢ�º���
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

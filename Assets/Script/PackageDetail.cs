using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    Transform Stars;
    Transform ShortDescript;
    Transform DetailDescript;
    Transform Icon;
    Transform Tittle;
    Transform Level;

    PackageTabItem packStaticInfo;
    PackageLocalItem packDynamicInfo;
    PackagePanel packUI;

    private void Awake()
    {
        
    }

    void InitDetailUI()
    {
        Stars = transform.Find("Center/Stars");
        ShortDescript = transform.Find("Center/ShortDescription");
        DetailDescript = transform.Find("Button/DetailDescription");
        Icon = transform.Find("Center/Icon");
        Tittle = transform.Find("Top/Name");
        Level = transform.Find("Button/Level");
    }

    public void Refresh(PackageLocalItem LocalInfo, PackagePanel uiPack)
    {
        packDynamicInfo = LocalInfo;
        packStaticInfo = GameManager.Instance.GetPackTabItem(LocalInfo.id);
        packUI = uiPack;
        // 刷新等级
        Level.GetComponent<TextMeshProUGUI>().text = "Lv." + packDynamicInfo.level + "/99";
        // 刷新简介
        ShortDescript.GetComponent<TextMeshProUGUI>().text = packStaticInfo.shortDescription;
        // 刷新详细介绍
        DetailDescript.GetComponent<TextMeshProUGUI>().text = packStaticInfo.detailDescription;
        // 刷新物品名称
        Tittle.GetComponent<TextMeshProUGUI>().text = packStaticInfo.name;
        // 刷新物品图标
        Texture2D ItemIcon = (Texture2D)Resources.Load(packStaticInfo.iconPath);
        Sprite temp = Sprite.Create(ItemIcon, new Rect(0, 0, ItemIcon.width, ItemIcon.height), new Vector2(0, 0));
        Icon.GetComponent<Image>().sprite = temp;
        // 刷新星级
        refreshStars();
    }
    // 星级刷新函数
    public void refreshStars()
    {
        for (int i = 0; i < Stars.childCount; i++)
        {
            Transform Star = Stars.GetChild(i);
            if (packStaticInfo.star > i)
            {
                Star.gameObject.SetActive(true);
            }
            else
            {
                Star.gameObject.SetActive(false);
            }
        }
    }
}

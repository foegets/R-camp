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
        // ˢ�µȼ�
        Level.GetComponent<TextMeshProUGUI>().text = "Lv." + packDynamicInfo.level + "/99";
        // ˢ�¼��
        ShortDescript.GetComponent<TextMeshProUGUI>().text = packStaticInfo.shortDescription;
        // ˢ����ϸ����
        DetailDescript.GetComponent<TextMeshProUGUI>().text = packStaticInfo.detailDescription;
        // ˢ����Ʒ����
        Tittle.GetComponent<TextMeshProUGUI>().text = packStaticInfo.name;
        // ˢ����Ʒͼ��
        Texture2D ItemIcon = (Texture2D)Resources.Load(packStaticInfo.iconPath);
        Sprite temp = Sprite.Create(ItemIcon, new Rect(0, 0, ItemIcon.width, ItemIcon.height), new Vector2(0, 0));
        Icon.GetComponent<Image>().sprite = temp;
        // ˢ���Ǽ�
        refreshStars();
    }
    // �Ǽ�ˢ�º���
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

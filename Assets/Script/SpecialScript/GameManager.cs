using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private PackageTable PackTab;
    public int NextSceneIndex;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        //UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }

    
    void Update()
    {
        
    }
    // 获取静态数据
    public PackageTable GetPackTabData()
    {
        if (PackTab == null)
        {
            PackTab = Resources.Load<PackageTable>("StaticData/PackageTable");
        }
        return PackTab;
    }
    // 获取动态数据
    public List<PackageLocalItem> GetPackLocalData()
    {
        return PackageLocalData.Instance.LoadPackageData();
    }
    // 通过ID获取指定物体的静态数据
    public PackageTabItem GetPackTabItem(int id)
    {
        List<PackageTabItem> TabItemList = GetPackTabData().DataList;
        foreach (PackageTabItem item in TabItemList)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
    // 通过UID获取指定物体的动态数据
    public PackageLocalItem GetPackLocalItem(string uid)
    {
        List<PackageLocalItem> LocalItemList = GetPackLocalData();
        foreach (PackageLocalItem item in LocalItemList)
        {
            if (item.uid == uid)
            {
                return item;
            }
        }
        return null;
    }
    // 获得排序好的背包物体List
    public List<PackageLocalItem> GetSortedPackLocalData()
    {
        List<PackageLocalItem> packLocalItems = PackageLocalData.Instance.LoadPackageData();
        packLocalItems.Sort(new PackageItemComparer());
        return packLocalItems;
    }
}
// 背包排序方式
public class PackageItemComparer : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem x, PackageLocalItem y)
    {
        PackageTabItem a = GameManager.Instance.GetPackTabItem(x.id);
        PackageTabItem b = GameManager.Instance.GetPackTabItem(y.id);
        // 首先比较星级
        int starCompare = b.star.CompareTo(a.star);
        // 如果星级相等则比较等级
        if (starCompare == 0)
        {
            int lvCompare = y.level.CompareTo(x.level);
            // 如果等级相同则比较ID
            if (lvCompare == 0)
            {
                int idCompare = x.id.CompareTo(y.id);
                return idCompare;
            }
            return lvCompare;
        }
        return starCompare;
    }
}

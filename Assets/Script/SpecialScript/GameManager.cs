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
    // ��ȡ��̬����
    public PackageTable GetPackTabData()
    {
        if (PackTab == null)
        {
            PackTab = Resources.Load<PackageTable>("StaticData/PackageTable");
        }
        return PackTab;
    }
    // ��ȡ��̬����
    public List<PackageLocalItem> GetPackLocalData()
    {
        return PackageLocalData.Instance.LoadPackageData();
    }
    // ͨ��ID��ȡָ������ľ�̬����
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
    // ͨ��UID��ȡָ������Ķ�̬����
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
    // �������õı�������List
    public List<PackageLocalItem> GetSortedPackLocalData()
    {
        List<PackageLocalItem> packLocalItems = PackageLocalData.Instance.LoadPackageData();
        packLocalItems.Sort(new PackageItemComparer());
        return packLocalItems;
    }
}
// ��������ʽ
public class PackageItemComparer : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem x, PackageLocalItem y)
    {
        PackageTabItem a = GameManager.Instance.GetPackTabItem(x.id);
        PackageTabItem b = GameManager.Instance.GetPackTabItem(y.id);
        // ���ȱȽ��Ǽ�
        int starCompare = b.star.CompareTo(a.star);
        // ����Ǽ������Ƚϵȼ�
        if (starCompare == 0)
        {
            int lvCompare = y.level.CompareTo(x.level);
            // ����ȼ���ͬ��Ƚ�ID
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData : MonoBehaviour
{
    private static PackageLocalData instance;

    public static PackageLocalData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PackageLocalData();
            }
            return instance;
        }
    }

    public List<PackageLocalItem> items;

    public void SavePackageData()
    {
        // 将表格信息序列化为字符串
        string inventoryJson = JsonUtility.ToJson(this);
        // 再把文本数据存储到本地的文件中
        PlayerPrefs.SetString("PackageLoaclData", inventoryJson);
        PlayerPrefs.Save();
    }

    public List<PackageLocalItem> LoadPackageData()
    {
        if (items != null)
        {
            return items;
        }
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJon = PlayerPrefs.GetString("PackageLocalData");
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJon);
            items = packageLocalData.items;
            return items;
        }
        else
        {
            items = new List<PackageLocalItem>();
            return items;
        }
    }
}

[System.Serializable]
public class PackageLocalItem
{
    public string uid;
    public int id;
    public int number;
    public int level;
    public bool isnew;
}

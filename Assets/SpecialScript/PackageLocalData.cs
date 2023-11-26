using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData : MonoBehaviour
{
    // 设置为单例模式
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

    // 创建存储背包物体信息的列表
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
        // 若过之前读取过，已经存在于缓存中，那么直接返回
        if (items != null)
        {
            return items;
        }
        // 否则就需要在本地读取文件
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            // 把本地的文件转成字符串
            string inventoryJon = PlayerPrefs.GetString("PackageLocalData");
            // 反序列化
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJon);
            items = packageLocalData.items;
            return items;
        }
        else
        {
            // 如果本地也没有，那么就新建吧
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

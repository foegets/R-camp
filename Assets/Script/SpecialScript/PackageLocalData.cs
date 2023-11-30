using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData
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
    // 创建存储背包物体动态信息的列表
    public List<PackageLocalItem> items;
    public void SavePackageData()
    {
        // 将表格信息序列化为字符串
        string inventoryJson = JsonUtility.ToJson(this);
        // 再把字符串数据存储到本地的文件中
        PlayerPrefs.SetString("PackageLocalData", inventoryJson);
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
        // 判断本地是否存在目标文件
        // 检查是否有目标键
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            // 把本地的文件里的字符串数据
            string inventoryJon = PlayerPrefs.GetString("PackageLocalData");
            // 反序列化
            // 将字符串数据转化成原本的类型的数据
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJon);
            // 将数据返回出去
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

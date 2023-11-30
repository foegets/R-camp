using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GMCmd
{
    [MenuItem("CMCmd/背包静态数据读取测试")]
    public static void LoadPackageTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("StaticData/PackageTable");
        if (packageTable == null )
        {
            Debug.Log("读取失败");
        }
        foreach (PackageTabItem packageTabItem in packageTable.DataList)
        {
            Debug.Log(string.Format("物体名称:{0}, 物体的Id:{1}, 物体简介:{2}", packageTabItem.name, packageTabItem.id, packageTabItem.shortDescription));
        }
    }
    [MenuItem("CMCmd/创建背包动态数据测试")]
    public static void CreateLocalPackData()
    {
        // 初始化存储动态数据的List
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        // 存储数据
        for (int i = 0;i < 9;i++)
        {
            // 创建数据
            PackageLocalItem PackLocalItem = new PackageLocalItem()
            {
                // Guid.NewGuid()用于随机生成一个唯一的标识符
                uid = Guid.NewGuid().ToString(),
                id = i,
                number = i,
                level = i,
                isnew = i % 2 == 0,
            }; 
            // 然后存储
            PackageLocalData.Instance.items.Add(PackLocalItem);
        }
        // 然后保存到本地
        PackageLocalData.Instance.SavePackageData();
    }
    [MenuItem("CMCmd/读取背包动态数据测试")]
    public static void LoadLocalPackData()
    {
        // 读取数据
        List<PackageLocalItem> readList = PackageLocalData.Instance.LoadPackageData();
        foreach (PackageLocalItem Item in readList)
        {
            Debug.Log(Item);
        }
    }
    [MenuItem("CMCmd/打开背包界面测试")]
    public static void OpenPackPanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }

}

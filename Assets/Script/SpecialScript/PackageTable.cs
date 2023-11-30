using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "菜单的名称"，fileName = "文件名称")]
// 允许在unity中使用右键来创建一个配置文件
[CreateAssetMenu(menuName = "StaticData/PackageTable", fileName = "PackageTable")]

public class PackageTable : ScriptableObject
{
    public List<PackageTabItem> DataList = new List<PackageTabItem>();
}

[System.Serializable]
public class PackageTabItem
{
    public int id;
    public int type;
    public int star;
    public string name;
    public string shortDescription;
    public string detailDescription;
    public string iconPath;
}
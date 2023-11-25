using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "菜单的名称"，fileName = "文件名称")]
// 允许在unity中使用右键来创建一个配置文件
[CreateAssetMenu(menuName = "BackpackTable", fileName = "BackpackTable")]

public class CreatePackageTableAssetMenu : ScriptableObject
{
    public List<PackageItem> PackageDateList = new List<PackageItem>();
}

[System.Serializable]
public class PackageItem
{
    public int id;
    public int type;
    public string name;
    public string description;
    public string iconpath;
    public int number;
}
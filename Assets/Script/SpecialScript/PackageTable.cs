using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "�˵�������"��fileName = "�ļ�����")]
// ������unity��ʹ���Ҽ�������һ�������ļ�
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "�˵�������"��fileName = "�ļ�����")]
// ������unity��ʹ���Ҽ�������һ�������ļ�
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
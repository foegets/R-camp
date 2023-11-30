using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GMCmd
{
    [MenuItem("CMCmd/������̬���ݶ�ȡ����")]
    public static void LoadPackageTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("StaticData/PackageTable");
        if (packageTable == null )
        {
            Debug.Log("��ȡʧ��");
        }
        foreach (PackageTabItem packageTabItem in packageTable.DataList)
        {
            Debug.Log(string.Format("��������:{0}, �����Id:{1}, ������:{2}", packageTabItem.name, packageTabItem.id, packageTabItem.shortDescription));
        }
    }
    [MenuItem("CMCmd/����������̬���ݲ���")]
    public static void CreateLocalPackData()
    {
        // ��ʼ���洢��̬���ݵ�List
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        // �洢����
        for (int i = 0;i < 9;i++)
        {
            // ��������
            PackageLocalItem PackLocalItem = new PackageLocalItem()
            {
                // Guid.NewGuid()�����������һ��Ψһ�ı�ʶ��
                uid = Guid.NewGuid().ToString(),
                id = i,
                number = i,
                level = i,
                isnew = i % 2 == 0,
            }; 
            // Ȼ��洢
            PackageLocalData.Instance.items.Add(PackLocalItem);
        }
        // Ȼ�󱣴浽����
        PackageLocalData.Instance.SavePackageData();
    }
    [MenuItem("CMCmd/��ȡ������̬���ݲ���")]
    public static void LoadLocalPackData()
    {
        // ��ȡ����
        List<PackageLocalItem> readList = PackageLocalData.Instance.LoadPackageData();
        foreach (PackageLocalItem Item in readList)
        {
            Debug.Log(Item);
        }
    }
    [MenuItem("CMCmd/�򿪱����������")]
    public static void OpenPackPanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }

}

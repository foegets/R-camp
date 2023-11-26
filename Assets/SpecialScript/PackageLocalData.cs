using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData : MonoBehaviour
{
    // ����Ϊ����ģʽ
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

    // �����洢����������Ϣ���б�
    public List<PackageLocalItem> items;

    public void SavePackageData()
    {
        // �������Ϣ���л�Ϊ�ַ���
        string inventoryJson = JsonUtility.ToJson(this);
        // �ٰ��ı����ݴ洢�����ص��ļ���
        PlayerPrefs.SetString("PackageLoaclData", inventoryJson);
        PlayerPrefs.Save();
    }

    public List<PackageLocalItem> LoadPackageData()
    {
        // ����֮ǰ��ȡ�����Ѿ������ڻ����У���ôֱ�ӷ���
        if (items != null)
        {
            return items;
        }
        // �������Ҫ�ڱ��ض�ȡ�ļ�
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            // �ѱ��ص��ļ�ת���ַ���
            string inventoryJon = PlayerPrefs.GetString("PackageLocalData");
            // �����л�
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJon);
            items = packageLocalData.items;
            return items;
        }
        else
        {
            // �������Ҳû�У���ô���½���
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData
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
    // �����洢�������嶯̬��Ϣ���б�
    public List<PackageLocalItem> items;
    public void SavePackageData()
    {
        // �������Ϣ���л�Ϊ�ַ���
        string inventoryJson = JsonUtility.ToJson(this);
        // �ٰ��ַ������ݴ洢�����ص��ļ���
        PlayerPrefs.SetString("PackageLocalData", inventoryJson);
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
        // �жϱ����Ƿ����Ŀ���ļ�
        // ����Ƿ���Ŀ���
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            // �ѱ��ص��ļ�����ַ�������
            string inventoryJon = PlayerPrefs.GetString("PackageLocalData");
            // �����л�
            // ���ַ�������ת����ԭ�������͵�����
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJon);
            // �����ݷ��س�ȥ
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

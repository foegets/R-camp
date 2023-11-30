using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    // ���õ���ģʽ
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    // ����UI�ĸ��ڵ�
    private Transform uiRoot;
    // �����ֵ�洢·��
    private Dictionary<string, string> PathDic;
    // UIԤ����Ļ����ֵ�
    private Dictionary<string, GameObject> PrefabDic;
    // �Ѵ򿪵�UI�Ļ����ֵ�
    public Dictionary<string, BasePanel> PanelDic;

    private UIManager()
    {
        InitDic();
    }
    // ��ʼ���ֵ�
    private void InitDic()
    {
        PrefabDic = new Dictionary<string, GameObject>();
        PanelDic = new Dictionary<string, BasePanel>();
        PathDic = new Dictionary<string, string>()
        {
            {UIConst.PausePanel, "Prefab/UI/PausePanel"},
            {UIConst.GuidePanel, "Prefab/UI/Guide Panel"},
            {UIConst.PackagePanel,"Prefab/UI/BackpackSystem"},
            {UIConst.SettingPanel, "Prefab/UI/Setting Panel"},
            {UIConst.DeathPanel, "Prefab/UI/DeadImage"}
        };
    }

    // ���ø��ڵ�
    public Transform UIRoot
    {
        get
        {
            if (uiRoot == null)
            {
                uiRoot = GameObject.Find("Player_UI").transform;
            }
            return uiRoot;
        }
    }

    // ����
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        // �ж��Ƿ�����Ƿ��
        if (PanelDic.TryGetValue(name, out panel))
        {
            Debug.Log("�����Ѵ�");
            return null;
        }
        string path = "";
        // �ж�·���Ƿ����
        if (!PathDic.TryGetValue(name, out path))
        {
            Debug.Log("·��������");
            return null;
        }
        if (path == null)
        {
            Debug.Log("δ�ҵ�Ŀ��·��");
        }
        GameObject panelPrefab = null;
        // �ж�Ԥ�����Ƿ��Ѿ�����
        if (!PrefabDic.TryGetValue(name, out panelPrefab))
        {
            panelPrefab = Resources.Load<GameObject>(path) as GameObject;
            PrefabDic.Add(name, panelPrefab);
        }
        if (panelPrefab ==  null)
        {
            Debug.Log("δ�ҵ�Ŀ��Ԥ����");
        }
        // �򿪽���
        GameObject UIPanel = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = UIPanel.GetComponent<BasePanel>();
        PanelDic.Add(name, panel);
        return panel;
    }

    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!PanelDic.TryGetValue (name, out panel))
        {
            Debug.Log("����δ��");
            return false;
        }
        if (panel == null)
        {
            Debug.Log("����Ϊ��");
        }
        panel.ClosePanel();
        return true;
    }
}

public class UIConst
{
    public const string PausePanel = "PausePanel";
    public const string GuidePanel = "GuidePanel";
    public const string PackagePanel = "PackagePanel";
    public const string DeathPanel = "DeathPanel";
    public const string SettingPanel = "SettingPanel";
}

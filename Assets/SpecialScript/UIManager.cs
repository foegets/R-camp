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
    // �����ֵ�洢·��
    private Dictionary<string, string> PanelPathDic;
    // UIԤ����Ļ����ֵ�
    private Dictionary<string, GameObject> PrefabPanelPathDic;
    // �Ѵ�UI�Ļ����ֵ�
    public Dictionary<string, BasePanel> ExistPanelPathDic;

    private UIManager()
    {
        InitData();
    }
    // ��ʼ���ֵ�
    private void InitData()
    {
        PrefabPanelPathDic = new Dictionary<string, GameObject>();
        ExistPanelPathDic = new Dictionary<string, BasePanel>();
        PanelPathDic = new Dictionary<string, string>()
        {
            {UIConst.PausePanel, "Assets/Prefab/UI/PausePanel.prefab"},
            {UIConst.GuidePanel, "Assets/Prefab/UI/Guide Panel.prefab"},
            {UIConst.BackagePanel,"Assets/Prefab/UI/BackpackSystem.prefab"},
            {UIConst.SettingPanel, "Assets/Prefab/UI/Setting Panel.prefab"},
            {UIConst.DeathPanel, "Assets/Prefab/UI/DeadImage.prefab"}
        };
    }

    // ����UI�ĸ��ڵ�
    private Transform uiRoot;
    public Transform UIRoot
    {
        get
        {
            if (uiRoot == null)
            {
                uiRoot = GameObject.Find("PlayerUI").transform;
            }
            return uiRoot;
        }
    }

    // ����

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        // �ж��Ƿ�����Ƿ��
        if (!ExistPanelPathDic.TryGetValue(name, out panel))
        {
            Debug.Log("�����Ѵ�");
            return null;
        }
        string path = "";
        // �ж�·���Ƿ����
        if (!PanelPathDic.TryGetValue(name, out  path))
        {
            Debug.Log("·��������");
            return null;
        }
        GameObject prefab = null;
        // �ж�Ԥ�����Ƿ��Ѿ�����
        if (!PrefabPanelPathDic.TryGetValue(name, out prefab))
        {
            string panelpath = path;
            prefab = Resources.Load<GameObject>(panelpath) as GameObject;
            PrefabPanelPathDic.Add(name, prefab);
        }

        // �򿪽���
        GameObject UIPanel = GameObject.Instantiate(prefab, UIRoot, false);
        panel = UIPanel.GetComponent<BasePanel>();
        ExistPanelPathDic.Add(name, panel);
        return panel;
    }

    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!ExistPanelPathDic.TryGetValue (name, out panel))
        {
            Debug.Log("����δ��");
            return false;
        }
        panel.ClosePanel();
        return true;
    }
}

public class UIConst
{
    public const string PausePanel = "PausePanel";
    public const string GuidePanel = "GuidePanel";
    public const string BackagePanel = "BackagePanel";
    public const string DeathPanel = "PausePanel";
    public const string SettingPanel = "SettingPanel";
}

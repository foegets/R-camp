using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    // 设置单例模式
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

    // 创建UI的根节点
    private Transform uiRoot;
    // 利用字典存储路径
    private Dictionary<string, string> PathDic;
    // UI预制体的缓存字典
    private Dictionary<string, GameObject> PrefabDic;
    // 已打开的UI的缓存字典
    public Dictionary<string, BasePanel> PanelDic;

    private UIManager()
    {
        InitDic();
    }
    // 初始化字典
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

    // 设置根节点
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

    // 开关
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        // 判断是否界面是否打开
        if (PanelDic.TryGetValue(name, out panel))
        {
            Debug.Log("界面已打开");
            return null;
        }
        string path = "";
        // 判断路径是否存在
        if (!PathDic.TryGetValue(name, out path))
        {
            Debug.Log("路径不存在");
            return null;
        }
        if (path == null)
        {
            Debug.Log("未找到目标路径");
        }
        GameObject panelPrefab = null;
        // 判断预制体是否已经存在
        if (!PrefabDic.TryGetValue(name, out panelPrefab))
        {
            panelPrefab = Resources.Load<GameObject>(path) as GameObject;
            PrefabDic.Add(name, panelPrefab);
        }
        if (panelPrefab ==  null)
        {
            Debug.Log("未找到目标预制体");
        }
        // 打开界面
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
            Debug.Log("界面未打开");
            return false;
        }
        if (panel == null)
        {
            Debug.Log("界面为空");
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

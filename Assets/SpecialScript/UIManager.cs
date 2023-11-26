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
    // 利用字典存储路径
    private Dictionary<string, string> PanelPathDic;
    // UI预制体的缓存字典
    private Dictionary<string, GameObject> PrefabPanelPathDic;
    // 已打开UI的缓存字典
    public Dictionary<string, BasePanel> ExistPanelPathDic;

    private UIManager()
    {
        InitData();
    }
    // 初始化字典
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

    // 设置UI的根节点
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

    // 开关

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        // 判断是否界面是否打开
        if (!ExistPanelPathDic.TryGetValue(name, out panel))
        {
            Debug.Log("界面已打开");
            return null;
        }
        string path = "";
        // 判断路径是否存在
        if (!PanelPathDic.TryGetValue(name, out  path))
        {
            Debug.Log("路径不存在");
            return null;
        }
        GameObject prefab = null;
        // 判断预制体是否已经存在
        if (!PrefabPanelPathDic.TryGetValue(name, out prefab))
        {
            string panelpath = path;
            prefab = Resources.Load<GameObject>(panelpath) as GameObject;
            PrefabPanelPathDic.Add(name, prefab);
        }

        // 打开界面
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
            Debug.Log("界面未打开");
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

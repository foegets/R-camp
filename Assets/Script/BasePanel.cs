using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // 判断界面是否已打开
    public bool isRemove = false;
    // 界面名称
    protected new string name;
    
    public virtual void OpenPanel(string name)
    {
        this.name = name;
        gameObject.SetActive(true);
    }
    public virtual void ClosePanel()
    {
        isRemove = true;
        gameObject.SetActive(false);
        Destroy(gameObject);

        if (UIManager.Instance.ExistPanelPathDic.ContainsKey(name))
        {
            UIManager.Instance.ExistPanelPathDic.Remove(name);
        }
    }

}

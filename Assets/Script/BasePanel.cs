using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // �жϽ����Ƿ��Ѵ�
    protected bool isRemove = false;
    // ��������
    protected new string name;
    
    public virtual void OpenPanel(string name)
    {
        this.name = name;
        gameObject.SetActive(true);
    }
    public virtual void ClosePanel()
    {
        isRemove = true;
        if (UIManager.Instance.PanelDic.ContainsKey(name))
        {
            UIManager.Instance.PanelDic.Remove(name);
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}

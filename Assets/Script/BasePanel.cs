using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // �жϽ����Ƿ��Ѵ�
    public bool isRemove = false;
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
        gameObject.SetActive(false);
        Destroy(gameObject);

        if (UIManager.Instance.ExistPanelPathDic.ContainsKey(name))
        {
            UIManager.Instance.ExistPanelPathDic.Remove(name);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;//Ѫ����
    public Image powerImage;//�������������Ժ��ã�
    /// <summary>
    /// ����health�ı���ٷֱ�
    /// </summary>
    /// <param name="persentage"></param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }
}

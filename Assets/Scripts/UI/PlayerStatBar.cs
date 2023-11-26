using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;//血量条
    public Image powerImage;//能量条（可能以后用）
    /// <summary>
    /// 接收health的变更百分比
    /// </summary>
    /// <param name="persentage"></param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }
}

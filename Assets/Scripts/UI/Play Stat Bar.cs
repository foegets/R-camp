using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;

    /// <summary>
    /// 接收Health的变更百分比
    /// </summary>
    /// <param name="persentage"></param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }

}

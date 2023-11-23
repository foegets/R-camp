using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    //创建图片
    public Image healthImage;//绿色那条
    public Image healthDelayImage;//红色有延迟那条
    public Image powerImage;//能量条

    //通过判断红条和绿条是否一致，不一致的话，让红条缓慢减去时间修正，达到一致为止
    private void Update()
    {
        if(healthDelayImage.fillAmount > healthImage.fillAmount)
            healthDelayImage.fillAmount -= Time.deltaTime;
    }

    /// <summary>
    /// 接收Health的变更百分比
    /// </summary>
    /// <param name = "persentage">百分比：Current/Max</param>
    //血量变更的时候才执行的函数
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }


}

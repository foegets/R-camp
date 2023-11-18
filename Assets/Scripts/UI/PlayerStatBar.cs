using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;
   
    ///<summary>
    ///</summary>
    ///<param name="persentage">百分比：Current/Max></param>   
    
    
    
    public void OnHealthChange(float persentage)//调整血量
    {
        healthImage.fillAmount = persentage;
    }
}

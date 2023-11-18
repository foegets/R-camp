using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;


    //由于绿条移动看fillAmount，且数值从0到1
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
}

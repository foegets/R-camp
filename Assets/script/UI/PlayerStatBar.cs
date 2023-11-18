using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;


    //���������ƶ���fillAmount������ֵ��0��1
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
}

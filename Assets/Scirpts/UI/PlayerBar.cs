using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBar : MonoBehaviour
{
    public float percentage;
    public Image healthImage;
    public Image healthDealyImage;
    public Image powerImage;

    public void OnHealthChange(float percentage)
    {
        Debug.Log("hello");
        Debug.Log(percentage);
        healthImage.fillAmount = percentage;
    }
}

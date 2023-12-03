using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatBar : MonoBehaviour
{
    [Header("与血量条相联系的变量")]
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;


    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount-=Time.deltaTime*0.5f;
        }
    }
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }
}

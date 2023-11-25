using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{
    // Start is called before the first frame update
    static public UI_manager Instance;
    public Image hp_mask;
    public Image mp_mask;
    public float original_size;//血条原始宽度
    void Awake() { 
        Instance = this;
        original_size=hp_mask.rectTransform.rect.width ;
        set_hp_value(0.5f);
    }
    private void set_hp_value(float percent)//血条ui填充显示
    {
        hp_mask.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,percent*original_size
            );
    }private void set_mp_value(float percent)//蓝条ui填充显示
    {
        hp_mask.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,percent*original_size
            );
    }
}

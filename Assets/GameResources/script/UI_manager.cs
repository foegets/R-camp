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
    public float original_size;//Ѫ��ԭʼ���
    void Awake() { 
        Instance = this;
        original_size=hp_mask.rectTransform.rect.width ;
        set_hp_value(0.5f);
    }
    private void set_hp_value(float percent)//Ѫ��ui�����ʾ
    {
        hp_mask.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,percent*original_size
            );
    }private void set_mp_value(float percent)//����ui�����ʾ
    {
        hp_mask.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,percent*original_size
            );
    }
}

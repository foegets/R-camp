using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Palyerstatbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthered;
    public Image healthegreen;

    public void OnHealthChange(float persentage)
    {
        healthegreen.fillAmount= persentage;

    }
}

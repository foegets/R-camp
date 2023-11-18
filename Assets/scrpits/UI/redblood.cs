using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class redblood : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Bar;
    private float maxhealth;
    private float currenthealth;
    private float changespeed = 3;

    // Update is called once per frame
    void Update()
    {
        barfiller();
    }
    private void barfiller()
    {
        maxhealth = GetComponent<character>().maxhealth;
        currenthealth = GetComponent<character>().currenthealth;
        Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, currenthealth / maxhealth, changespeed * Time.deltaTime);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossblood1 : MonoBehaviour
{
    public Image Bar;
    private float maxhealth;
    private float currenthealth;
    private float changespeed = 6;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Barfiller();
    }
    private void Barfiller()
    {
        maxhealth=GetComponent<character>().maxhealth;
        currenthealth=GetComponent<character>().currenthealth;
        if (currenthealth <= 1000)
            Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, 0, changespeed * Time.deltaTime);
        if (currenthealth < 2000)
            Bar.fillAmount = Mathf.Lerp(Bar.fillAmount,(currenthealth-1000)/1000,changespeed*Time.deltaTime);
    }
}

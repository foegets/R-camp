using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class greenblood : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Bar;
    private float maxhealth;
    private float currenthealth;
    private float changespeed = 6;




    // Update is called once per frame

    private void FixedUpdate()
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

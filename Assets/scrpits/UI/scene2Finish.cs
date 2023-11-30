using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scene2Finish : MonoBehaviour
{
    public GameObject victoryscreen;
    public float timecounter;
    public bool start;
    public void Awake()
    {
        timecounter = 7f;
        start = false;
    }
    public void FixedUpdate()
    {
        if(start==true) 
        timecounter -= Time.deltaTime;
        if(timecounter<=0)
        victoryscreen.SetActive(true);
    }
    // Start is called before the first frame update
    public void finish()
    {
        Debug.Log("1");
        start = true;
    }

}

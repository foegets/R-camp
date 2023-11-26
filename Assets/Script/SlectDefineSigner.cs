using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonMonitor : MonoBehaviour
{
    public bool isTrigger;
    float StartTime;
    public float ElapedTime;
    public GameObject firstSign;
    public GameObject secondSign;
    public GameObject thirdSign;
    
    // Start is called before the first frame update
    void Start()
    {
        isTrigger = false;
        StartTime = 0f;
        ElapedTime = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            ElapedTime = Time.time - StartTime;
        }
        if (isTrigger && ElapedTime > 0.5f && ElapedTime < 1f)
        {
            firstSign.SetActive(true);
            secondSign.SetActive(false);
            thirdSign.SetActive(false);
        }
        else if (isTrigger && ElapedTime >= 1f && ElapedTime < 1.5f)
        {
            firstSign.SetActive(false);
            secondSign.SetActive(true);
            thirdSign.SetActive(false);
        }
        else if (isTrigger && ElapedTime >= 1.5f)
        {
            firstSign.SetActive(false);
            secondSign.SetActive(false);
            thirdSign.SetActive(true);
        }
        else if (ElapedTime <= 0.5f)
        {
            firstSign.SetActive(false);
            secondSign.SetActive(false);
            thirdSign.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ElapedTime = 0;
            StartTime = Time.time;
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTrigger = false;
            ElapedTime = 0;
        }
    }

    
}

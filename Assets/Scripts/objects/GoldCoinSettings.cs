using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinSettings : MonoBehaviour
{
    private Transform trans;
    private float goldScale;
    public bool isEngage;
    private void Awake()
    {
        trans=GetComponent<Transform>();
        goldScale = 1;
        isEngage = false;
    }

    void Update()
    {
        Rotating();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            this.isEngage = true;
            this.gameObject.SetActive(false);
        }

    }

    public void Rotating()
    {
        if(trans.localScale.x<=1 && trans.localScale.x>=-1) 
        {
            goldScale -= 0.035F;
        }
        else
        {
            goldScale = 1;
        }
        trans.localScale = new Vector3 (goldScale, 1,1);
    }

   
}

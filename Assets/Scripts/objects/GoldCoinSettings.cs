using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinSettings : MonoBehaviour
{
    private Transform trans;
    private float goldScale;
    public float coinSize;
    private void Awake()
    {
        trans=GetComponent<Transform>();
        goldScale = coinSize;
    }

    void Update()
    {
        Rotating();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            this.gameObject.SetActive(false);
        }

    }

    public void Rotating()
    {
        if(trans.localScale.x<= coinSize && trans.localScale.x>=-coinSize) 
        {
            goldScale -= 0.035F* coinSize;
        }
        else
        {
            goldScale = coinSize;
        }
        trans.localScale = new Vector3 (goldScale, coinSize, 1);
    }

   
}

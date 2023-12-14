using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float blood;
    public float damage;

    public float flashTime;
    private SpriteRenderer sr;
    private Color oriColor;
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        oriColor = sr.color;
    }

    protected void Update()
    {
        if(blood<=0)
        {
            Destroy(gameObject);
        }
    }

    public void GetDamage(float damage)
    {
        blood -= damage;
        FlashColor();
    }

    void FlashColor()
    {
        sr.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    public void ResetColor()
    {
       
        sr.color = oriColor;
    }

    abstract public void Attack();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class disappear : MonoBehaviour
{
    
    public float DisappearTime;
    public bool Disappear;

    public void Awake()
    {
     
    }
    public void Update()
    {
        if(Disappear==true)
       DisappearTime-=Time.deltaTime;

        disappear1();
    }
    public void FixedUpdate()
    {
        if (Disappear == true)
            DisappearTime -= Time.deltaTime;

        disappear1();
    }
    // Start is called before the first frame update
    public void Disapper()
    {
        Disappear=true;
        
    }
    public void disappear1()
    {
        if (DisappearTime <= 0)
        {
            Destroy(gameObject);

     

        }
    }
}

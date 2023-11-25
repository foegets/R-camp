using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public Animator cameraanim;
    // Start is called before the first frame update
    void Start()
    {
        //cameraanim = GameObject.FindGameObjectWithTag("MainCamara").GetComponent<Animator>();
    }
    public void Shake()
    {
        cameraanim.SetTrigger("isshake");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public int chealth, mhealth;
    // Start is called before the first frame update
    void Start()
    {
        chealth = mhealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void dealdamage()
    {
        chealth--;
        if (chealth <= 0)
        {
            gameObject.SetActive(false);
        }
     }

}

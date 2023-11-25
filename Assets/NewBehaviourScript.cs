using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.localScale.x > 0)
        {
        transform.localScale = new Vector3(1,1,1);
        }
         if (Player.transform.localScale.x < 0)
        {
             transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}

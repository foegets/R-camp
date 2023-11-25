using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject show_effect;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(show_effect,transform.position,Quaternion.identity);
        luna_controler luna = other.GetComponent<luna_controler>();
        //controler luna = other.GetComponent<controler>();
        //Debug.Log("test");
        if (luna != null)
        {
            luna.add_point();
            Destroy(gameObject);
        }
    }

}

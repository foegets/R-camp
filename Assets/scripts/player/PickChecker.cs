using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickChecker : MonoBehaviour
{
    public GameObject pickableitem;
    public GameObject item;
    public GameObject pickTip;

    public float tipPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PickableItem")
        {
            Debug.Log("itemHere");
            Instantiate(pickTip,transform.position+new Vector3(0,tipPosition,0),transform.rotation,transform);
            pickableitem = collision.gameObject;
            item = pickableitem?.GetComponent<GetItem>().item;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PickableItem")
        {
            Debug.Log("itemExit");
            pickableitem = null;
            item = null;
        }
    }

}

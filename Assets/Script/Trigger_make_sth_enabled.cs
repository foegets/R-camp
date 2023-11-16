using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_make_sth_enabled : MonoBehaviour
{
    public GameObject goj;
    private bool ifActive;
    // Start is called before the first frame update
    void Start()
    {
        ifActive = goj.activeSelf;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            goj.SetActive(!ifActive);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

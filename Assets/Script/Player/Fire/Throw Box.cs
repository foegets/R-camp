using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBox : MonoBehaviour
{
    public Rigidbody2D rb;
    private GameObject boxPrefab;
    private void Start()
    {
        boxPrefab = (GameObject)Resources.Load("Prefab/Box");
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 body = rb.transform.position;
            Vector3 fire = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject box = Instantiate(boxPrefab, body, Quaternion.identity);
            box.GetComponent<Rigidbody2D>().velocity = (fire - body) * 3;
            Destroy(box, 3);
        }
    }
}

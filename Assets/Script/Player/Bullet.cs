using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    private GameObject bulletPrefab;
    public Rigidbody2D rb;
    public Animator animator;

    private void Start()
    {
        bulletPrefab = (GameObject)Resources.Load("Prefab/Arrow");
        

    }
  

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            
            Vector3 body = rb.transform.position;
            Vector3 fire=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject bullet = Instantiate(bulletPrefab, body, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = (fire - body) * 3;
            Destroy(bullet,3);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float arrowSpeed;
    private GameObject bulletPrefab;
    private bool isFire=true;
    public Rigidbody2D rb;
    public Animator animator;
    public float angle;

    private void Start()
    {
        bulletPrefab = (GameObject)Resources.Load("Prefab/Arrow");
    }
   

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isFire",isFire);
            Vector3 body = rb.transform.position;
            Vector3 fire=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (fire - body).normalized;

            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject bullet = Instantiate(bulletPrefab, body, Quaternion.identity);
           
            bullet.GetComponent<Rigidbody2D>().velocity = (fire - body) * arrowSpeed;
            bullet.GetComponent<Rigidbody2D>().rotation = angle * Mathf.Rad2Deg;
            Debug.Log(angle);
            Destroy(bullet,3);
        }
        else if (!Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isFire", !isFire);
        }
    }
    
    
}

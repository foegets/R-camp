using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class attack : MonoBehaviour
{


    public float attackRange;
    public float attackRate;
    private CircleCollider2D Enemy;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Enemy.enabled = false;
            Destroy(gameObject);

        }
    }
}

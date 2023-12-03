using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleControler : MonoBehaviour
{
    public float moveSpeed;

    public Transform onPoint, dowmPoint;

    public Rigidbody2D theRB;

    private Spikes Spikes;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (theRB.transform.position.y < dowmPoint.position.y)
        {
            if (moveSpeed<0)
            {
                moveSpeed = -1 * moveSpeed;
            }
        }
        if (theRB.transform.position.y > onPoint.position.y)
        {
            if (moveSpeed>0)
            {
                moveSpeed = -1 * moveSpeed;
            }
        }
            
     theRB.velocity = new Vector2(0, moveSpeed);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Spikes.instance.isDied = true;
            Spikes.instance.DIE();
            Time.timeScale = 0f;
        }



    }

}

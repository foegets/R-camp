using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [Header("基本参数")]
    public GameObject summonObject;

    public int summonLimit;

    public Vector2 summonPosition;

    private Vector2 position;
    // Start is called before the first frame update
    private void Awake()
    {
        position = (Vector2)transform.position + summonPosition;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"&&summonLimit>0)
        {
            Instantiate(summonObject,position,transform.rotation);
            summonLimit--;
        }
    }
}

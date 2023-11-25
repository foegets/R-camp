using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] bool isArrive;

    private void Update()
    {
        if (isArrive)
        {
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
            if (transform.position.y == b.position.y)
            {
                isArrive = !isArrive;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
            if(transform.position.y == a.position.y)
            {
                isArrive = !isArrive;
            }
        }
    }
}

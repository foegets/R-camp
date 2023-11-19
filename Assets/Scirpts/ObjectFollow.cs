using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{

    public Transform Player;
    Vector3 distance;
    public float smooth=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position-Player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position+distance, smooth);
    }
}

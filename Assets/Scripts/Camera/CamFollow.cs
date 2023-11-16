using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform lookAt;
    [SerializeField] float smooth = 0.02f;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Vector2.Lerp(transform.position, lookAt.position, smooth);
        pos.z = transform.position.z;
        transform.position = pos;
    }
}

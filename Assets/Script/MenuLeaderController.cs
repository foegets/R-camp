using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLeaderController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 MoveDir;
    public float MoveSpeed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (MoveDir.magnitude > 0.1f)
        {
            transform.Translate(MoveDir.normalized * MoveSpeed * Time.deltaTime);
        }
    }
}

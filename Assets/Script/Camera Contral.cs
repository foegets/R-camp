using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmeraContral : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject lookat;
    [SerializeField] float smooth;

    private void LateUpdate()
    {
        if (lookat.transform.position.x>-25)
        {
            Vector3 pos = transform.position;
            pos = Vector3.Lerp(pos, lookat.transform.position, smooth);
            pos.z = -10;
            transform.position = pos;
        }
        else
        {
            Vector3 pos=transform.position;
            pos.x = -8;
            pos.y = 2;
            pos.z = -25;
            Debug.Log("You Are Loss");
        }
    }
}

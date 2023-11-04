using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    [SerializeField] GameObject lookat;
    [SerializeField] float smooth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos = Vector3.Lerp(pos, lookat.transform.position, smooth);
        pos.z = -10;
        transform.position = pos;

    }

    private void LateUpdate()
    {


    }

}

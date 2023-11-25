using UnityEngine;
using System.Collections;

public class calculateMovingPlatformVelocity : MonoBehaviour
{
    private Vector3 m_pos;
    private Vector3 m_posF;
    public Vector3 m_velocity;

    // Use this for initialization
    void Start()
    {
    }

    void FixedUpdate()
    {
        //Debug.Log (gameObject.GetComponent<Rigidbody>().velocity);//the velocity is zero!!! so we cant use it
        m_posF = m_pos;
        m_pos = gameObject.transform.position;
        m_velocity = (m_pos - m_posF) / Time.fixedDeltaTime;

    }
}

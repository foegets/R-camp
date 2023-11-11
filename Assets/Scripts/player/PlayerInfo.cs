using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Vector3 playerPosition;
    void Update()
    {
        playerPosition = transform.position;
    }
}

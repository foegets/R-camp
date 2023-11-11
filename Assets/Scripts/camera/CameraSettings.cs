using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    private Vector3 cameraPosition;
    private PlayerInfo playerInfo;
    private void Awake()
    {
       playerInfo = GetComponent<PlayerInfo>();
    }
    private void Update()
    {
        cameraPosition = playerInfo.playerPosition;
        cameraPosition.z -= 10;
        transform.position = cameraPosition;
    }
}

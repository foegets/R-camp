using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBombTrap : MonoBehaviour
{
    private Collider2D cd;
    public GameObject Trap1;

    public void OnTriggerEnter2D(Collider2D Player){
        for(int i = 0;i<=2;i++){
        Instantiate(Trap1, new Vector3(transform.position.x + i + 2, transform.position.y, 0), Quaternion.identity);
        }
        Destroy(gameObject,0.005f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
    public GameObject Trap;
    float minHeight = -2;
    public float spawnRate;
    // Start is called before the first frame update
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(Trap, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * minHeight;
    }
}

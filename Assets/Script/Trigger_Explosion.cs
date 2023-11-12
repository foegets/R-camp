using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 触发爆炸 : MonoBehaviour
{
    public GameObject Prefab_bomb;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 pos = new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z);
            Instantiate(Prefab_bomb, pos, Quaternion.identity);
            StartCoroutine(DelayDestroy(7.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DelayDestroy(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Destroy(Prefab_bomb);
    }
}

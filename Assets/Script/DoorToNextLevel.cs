using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevel : MonoBehaviour
{

    bool isInDoor;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        isInDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        InDoor();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //检测人物
        if (other.gameObject.CompareTag("Player"))
        {
            isInDoor = true;
            Player = other.gameObject.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //检测人物
        if (other.gameObject.CompareTag("Player"))
        {
            isInDoor = false;
            Player = null;
        }
    }

    void InDoor()
    {
        if (isInDoor)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }
    }
}

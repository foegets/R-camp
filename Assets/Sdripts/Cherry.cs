using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    public bool isCherry;
    private UIController uIController;
    // public bool isCollected=false;

    // Start is called before the first frame update
    void Start()
    {
        uIController = UIController.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("player"))
        {


            if (isCherry)
            {

                Destroy(gameObject);
                LevelManeger.gensCollected++;
                LevelManeger.gensCollected++;
                UIController.gensCollected++;
                UIController.gensCollected++;
            }

        }
    }
}
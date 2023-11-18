using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen2 : MonoBehaviour
{
    public bool isGen;
    public GameObject prefaCherry;
    // public bool isCollected=false;
    private UIController uIController;
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


            if (isGen) 
            {
                Destroy(gameObject);
                LevelManeger.gensCollected++;
                UIController.gensCollected++;

                GameObject cherry_0 = Instantiate(prefaCherry);
                cherry_0.transform.position = new Vector2(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y+3);
            }
        }//
    }



}

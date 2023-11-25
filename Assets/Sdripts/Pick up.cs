using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Pickup : MonoBehaviour
{
    private UIController uIController;
    public bool isGen;
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


            if (isGen)
            {
                
                Destroy(gameObject);
                LevelManeger.gensCollected++;
            }
        }
    }




}
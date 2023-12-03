using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Pickup : MonoBehaviour
{
    public bool isGen;
    public bool isCherry;
    void Start()
    {
        
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
                Destroy(gameObject); //销毁依附的gameObject
                LevelManeger.gensCollected++;
            }
            if (isCherry)
            {
                Destroy(gameObject); //销毁依附的gameObject
                LevelManeger.gensCollected++;
                LevelManeger.gensCollected++;
            }
        }
    }




}
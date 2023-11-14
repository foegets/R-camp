using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Trigger : MonoBehaviour
{
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player_Status_Monitor Player_Status = other.GetComponent<Player_Status_Monitor>();
            Player_Status.Player_Engry.value += 1;
            audio.Play();
            Destroy(gameObject, 0.2f);
        }
    }
}

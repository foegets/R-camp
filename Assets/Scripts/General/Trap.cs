using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    private Character character;
    [Header("œ›⁄Â Ù–‘")]
    public int spikeHurt;

    private void Start()
    {
        character = GetComponent<Character>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Spike")
        {
            character.currentHealth -= spikeHurt; 
        }
    }
}

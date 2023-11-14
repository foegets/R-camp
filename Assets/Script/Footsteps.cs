using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public GameObject walksteps;
    public GameObject runsteps;
    public GameObject jumpupsteps;
    public GameObject jumpdownsteps;
    AudioSource walksound;
    AudioSource runsound;
    AudioSource jumpupsound;
    AudioSource jumpdownsound;
    // Start is called before the first frame update
    void Start()
    {
        walksound = walksteps.GetComponent<AudioSource>();
        runsound = runsteps.GetComponent<AudioSource>();
        jumpdownsound = jumpdownsteps.GetComponent<AudioSource>();
        jumpupsound = jumpupsteps.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void walk()
    {
        walksound.Play();
    }
    void run()
    {
        runsound.Play();
    }
    void jumpup()
    {
        jumpupsound.Play();
    }
    void jumpdown()
    {
        jumpdownsound.Play();
    }
}

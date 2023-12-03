using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionChecker : MonoBehaviour
{
    public Score score;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("awake");
        score = transform.parent.GetComponent<Score>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collection)
    {
        if (collection.tag == "Collections")
        { 
            score.totalScore += score.coinScore;
            Destroy(collection.gameObject);
        }
    }
}

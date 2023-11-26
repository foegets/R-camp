using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{

    public Character character;

    private void Awake()
    {

        character.GetComponent<Character>();
    }

    private void Update()
    {
        transform.localScale = new Vector3( 5*(float)character.health/(float)character.maxHealth,3 , 1);
    }
}

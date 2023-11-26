using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striction : MonoBehaviour
{
    [SerializeField] int Health=10;
    public Rigidbody2D body;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trap")
        {
            Health--;
        }//要做到碰到collision之后彻底改变运动性质
        if (collision.tag == "Heart")
        {
            Health++;
        }
    }
    private void Update()
    {
        if (Health == 0)
        {
            body.gameObject.SetActive(false);
        }
    }
}

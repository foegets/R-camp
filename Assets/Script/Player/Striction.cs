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
        }//Ҫ��������collision֮�󳹵׸ı��˶�����
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

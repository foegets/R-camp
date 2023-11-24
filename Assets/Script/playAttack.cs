using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAttack : MonoBehaviour
{
    private Animator playerani;
    private PolygonCollider2D poly2d;

    public float damage;
    public float startTime;
    public float endTime;

    void Start()
    {
        playerani = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        poly2d = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {

        Attack();
    }

    void Attack()
    {
        if(Input.GetButtonDown("Attack"))
        {
            
            playerani.SetTrigger("isAttack");
            StartCoroutine(startAttack());
        }
    }

    IEnumerator startAttack()
    {
        yield return new WaitForSeconds(startTime);
        poly2d.enabled = true;
        StartCoroutine(disableHitbox());
    }

    IEnumerator disableHitbox()
    {
        yield return new WaitForSeconds(endTime);
        poly2d.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage(damage);
        }
    }
}

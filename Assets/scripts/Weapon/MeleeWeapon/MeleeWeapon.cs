using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [HideInInspector] public Vector3 fireDir;

    protected float angle;

    public float x;

    protected override void Awake()
    {
        x = 0;
        base.Awake();
        fireDir = GameObject.Find("player").GetComponent<PlayerController>().fireDir;
        angle = Mathf.Atan2(fireDir.y, fireDir.x);
        rb.rotation = angle * Mathf.Rad2Deg+220;

        Destroy(this.gameObject, weaponAttackRange);
    }

    protected override void Update()
    {
        base.Update();
        transform.position = transform.parent.transform.position+ new Vector3(x,1.5f,0);
    }

    private void OnDisable()
    {
        if(transform.parent != null)
            transform.parent.gameObject.GetComponent<PlayerController>().isableusemelee = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().TakeDamage(this);
        }
    }

    public virtual Vector3 summonpos(GameObject player)
    {
        Vector3 summonpos = player.transform.position + new Vector3(0, 1.5f, 0);
        //Debug.Log(firepos+"firepos");
        //Debug.Log("RemoteWeapon Fire");
        //Debug.Log(player.gameObject.name+" "+"GameObject");
        //Instantiate(PreFab,firepos,quaternion);

        return summonpos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowJoustingLance : MeleeWeapon
{
    public GameObject player;
    public Rigidbody2D rig;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("player");
        rig = player.GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
        base.Update();
        weaponDamage = weaponDamage+Mathf.Abs(rig.velocity.x);
        Debug.Log(weaponDamage);
    }
}

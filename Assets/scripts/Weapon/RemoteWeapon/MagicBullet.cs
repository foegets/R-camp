using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : RemoteWeapon
{
    private MagicBulletAudioManager audioManager;

    private int a = 1;
    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        audioManager = transform.Find("MagicBulletAudioManager").GetComponent<MagicBulletAudioManager>();
        audioManager.PlayFireFXsoure();
    }

    protected override void Update()
    {
        base .Update();
    }

    protected override void hit()
    {
        base.hit();
        
        Collider2D enterCollider = Physics2D.OverlapCapsule((Vector2)transform.position + new Vector2(0.25f, -0.05f), new Vector2(3f, 1f), CapsuleDirection2D.Horizontal, 0, LayerMask);
        if (enterCollider&&enterCollider.tag != "Player"&&a==1)
        {
            Debug.Log("HitFX");
            audioManager.PlayHitFXsoure();
            a = -a;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : ItemBase
{
    [SerializeField] Rigidbody2D rb;
    private static System.Random random = new System.Random();
    public virtual void OnCollect(PlayerController player, bool sucess)
    {
        if (sucess) Destroy(gameObject);
        
    }

    public DroppedItem RandomMove()
    {
        rb.velocity = new Vector2(random.Next()%2 == 1 ? 1 : -1, 1) * 3;
        return this;
    }

    

    public static DroppedItem Create(string prefab_path)
    {
        GameObject i = Instantiate(Resources.Load<GameObject>(prefab_path));
        i.name = prefab_path;
        return i.GetComponent<DroppedItem>();
    }
}

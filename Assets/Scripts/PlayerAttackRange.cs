using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Collider2D range;
    public delegate bool HitObject(Collider2D collider);
    HitObject OnHit;

    HashSet<GameObject> hited = new HashSet<GameObject>();

    void Awake()
    {
        range.enabled = false;
    }
    public void StartAtk(HitObject onHit)
    {
        hited.Clear();
        OnHit = onHit;
        range.enabled = true;
    }

    public void EndAtk()
    {
        range.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnHit != null && !hited.Contains(collision.gameObject))
        {
            hited.Add(collision.gameObject);
            OnHit(collision);
        }
    }
}

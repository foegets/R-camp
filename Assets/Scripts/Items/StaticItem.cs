using UnityEngine;

public class StaticItem : ItemBase
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                OnHitPlayer(player);
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnHitPlayer(PlayerController player)
    {
        player.Inventory.PushItem(this);
    }
}

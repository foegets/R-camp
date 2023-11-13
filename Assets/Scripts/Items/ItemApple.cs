using UnityEngine;

public class ItemApple : ItemBase
{
    // Start is called before the first frame update
   

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("hit");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.Inventory.PushItem(this);
                Destroy(gameObject);
            }
        }
    }

}

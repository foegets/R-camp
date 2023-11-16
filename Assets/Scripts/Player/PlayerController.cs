using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Instancies")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] InventoryData inventory;

    [Header("Arg")]
    [SerializeField] float speed = 7;


    public InventoryData Inventory { get { return inventory; } }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        InventoryUI.Instance.inventoryData = inventory;  //设置物品栏UI显示的背包
        InventoryUI.Instance.InitUI();                   //生成格子
    }

    // Update is called once per frame
    void Update()
    {
        float input_x = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(input_x * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 8);
        }
    }
}

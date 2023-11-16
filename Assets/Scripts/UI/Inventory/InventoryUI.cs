using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static InventoryData;

public class InventoryUI : MonoBehaviour, IPointerClickHandler
{
    public static InventoryUI Instance;
    [SerializeField] GameObject prefab_Slot;
    [SerializeField] RectTransform slotContainer;
    [SerializeField] GameObject droped_range;


    [SerializeField] Image currentShow;
    [SerializeField] Text currentCount;

    [SerializeField] Vector2 slotStartPos;
    [SerializeField] Vector2 slotCellSize;

    [Header("Description")]
    [SerializeField] GameObject description;
    [SerializeField] Text descriptionText;
    [SerializeField] Text nameText;




    [NonSerialized]
    public InventoryData inventoryData = null;
    [NonSerialized]
    public ItemStack current = ItemStack.Empty;
    [NonSerialized]
    public ItemStack descriptionStack = ItemStack.Empty;


    HashSet<Slot> slots = new HashSet<Slot>();

    private void Awake()
    {
        Instance = this;
    }

    public void InitUI()  //初始化
    {
        if (slots.Count > 0) DestroyAllSlots();   //清除旧的
        if (inventoryData != null)
        {
            print("Init inventory slots");
            slotContainer.sizeDelta = new Vector2(
                                slotStartPos.x + slotCellSize.x * inventoryData.shape.x,
                                slotStartPos.y + slotCellSize.y * inventoryData.shape.y);

            for(int i = 0; i < inventoryData.shape.y; i++)      //摆格子
            {
                for (int j = 0; j < inventoryData.shape.x; j++)
                {
                    slots.Add(CreateSlot(
                                slotStartPos.x + slotCellSize.x * j,
                                -(slotStartPos.y + slotCellSize.y * i),
                                j + i * inventoryData.shape.x));
                }
            }
        }
    }

    Slot CreateSlot(float x, float y, int index)
    {
        GameObject obj = GameObject.Instantiate(prefab_Slot, slotContainer);
        obj.transform.localPosition = new Vector3(x,y,0);
        Slot slot = obj.GetComponent<Slot>();
        slot.Init(index);
        return slot;
    }


    void DestroyAllSlots()
    {
        foreach (Slot slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        FlushAllSolts();
        UpdateCurrentShow();
        UpdateCurrentDescription();
    }

    void FlushAllSolts()
    {
        if (inventoryData)
        {
            foreach (Slot slot in slots)
            {
                if (inventoryData.dirty[slot.index])  //仅在信息被修改时更新
                {
                    slot.itemStack = inventoryData.data[slot.index];
                    slot.Flush();
                }
            }
        }
    }

    void UpdateCurrentShow()    //更新手持物品显示
    {
        if (!current.isEmpty)
        {
            currentShow.enabled = true;
            currentCount.enabled = true;
            ItemManager.ItemRegistry reg = ItemManager.Instance.GetItemRegistry(current);
            if (reg != null) currentShow.sprite = reg.ImgOnGUI;
            currentShow.transform.position = Input.mousePosition;
            currentCount.text = string.Format("{0}", current.count);
        }
        else
        {
            currentShow.enabled = false;
            currentCount.enabled = false;
        }

    }

    void UpdateCurrentDescription()    //更新物品信息显示
    {
        if (!descriptionStack.isEmpty)
        {
            description.SetActive(true);
            ItemManager.ItemRegistry reg = ItemManager.Instance.GetItemRegistry(descriptionStack);
            if (reg != null)
            {
                descriptionText.text = reg.description;
                nameText.text = reg.name;
            }
            description.transform.position = Input.mousePosition;
        }
        else
        {
            description.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!current.isEmpty && eventData.pointerEnter == droped_range)
        {
            current.count--;
            ItemManager.Instance.CreateDropItem(current).RandomMove().transform.position = inventoryData.transform.position + Vector3.up;
            
            if (current.count <= 0)
            {
                current = ItemStack.Empty;
            }
        }
    }
}

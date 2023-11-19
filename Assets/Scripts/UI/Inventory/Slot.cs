using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //                                    鼠标进入推出事件的接口


    [SerializeField] Image sprite;
    [SerializeField] Button button;
    [SerializeField] Text countNumber;

    public int index;
    public InventoryData.ItemStack itemStack {        //字段映射
        get { return GetItemStack(); }
        set { SetItemStack(value); }
    }

    protected virtual InventoryData.ItemStack GetItemStack()
    {
        return InventoryUI.Instance.inventoryData.data[index];
    }

    protected virtual void SetItemStack(InventoryData.ItemStack itemStack)
    {
        InventoryUI.Instance.inventoryData.data[index] = itemStack;
    }

    public void Init(int idx)
    {
        index = idx;
        Flush();
    }

    public void Flush()  //刷新格子显示
    {
        ItemManager.ItemRegistry reg = ItemManager.Instance.GetItemRegistry(itemStack);
        if(reg != null)
        {
            sprite.sprite = reg.ImgOnGUI;
            sprite.color = Color.white;
            countNumber.text = string.Format("{0}", itemStack.count);
            countNumber.enabled = true;
        }
        else
        {
            sprite.sprite = null;
            sprite.color = new Color(0,0,0,0);
            countNumber.enabled = false;
        }
        
    }

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if(InventoryUI.Instance.current.isEmpty)   //当前手上上没东西，把格子里东西拿出来
        {
            InventoryUI.Instance.current = itemStack;
            itemStack = InventoryData.ItemStack.Empty;
        }
        else
        {
            if (itemStack.isEmpty)                //当前手上上有东西，但格子里面没东西，把手上的放进去
            {
                itemStack = InventoryUI.Instance.current;
                InventoryUI.Instance.current = InventoryData.ItemStack.Empty;
            }
            else                                   //当前手上上有东西，格子里面有东西，合两堆东西
            {
                InventoryUI.Instance.current = itemStack.Merge(InventoryUI.Instance.current);
            }
        }
        Flush();
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)     //鼠标进入添加描述
    {
        InventoryUI.Instance.descriptionStack = itemStack;
    }

    public void OnPointerExit(PointerEventData eventData)       //鼠标进入置空描述
    {
        InventoryUI.Instance.descriptionStack = InventoryData.ItemStack.Empty;
    }
}

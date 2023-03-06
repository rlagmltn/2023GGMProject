using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InterfaceType
{
    Inventory,
    Equipment,
    QuickSlot,
    Bag
}

[CreateAssetMenu(fileName ="New InventoryObject", menuName ="Inventory System/Inventory/InventoryObject")]
public class InventoryObj : ScriptableObject
{
    public ItemDBObj itemDBObj;
    public InterfaceType type;

    [SerializeField]
    private Inventory inventory = new Inventory();
    public InventorySlot[] inventorySlots => inventory.inventorySlots;

    public int GetEmptySlotCount
    {
        get
        {
            int cnt = 0;
            foreach(InventorySlot slot in inventorySlots)
            {
                if (slot.item.ar_id <= -1)
                    cnt++;
            }
            return cnt;
        }
    }

    public bool AddItem(Player item, int amount)
    {
        InventorySlot invenSlot = SearchItemInInven(item);
        if( !itemDBObj.itemObjects[item.ar_id].flagStackable || invenSlot == null)
        {
            if (GetEmptySlotCount <= 0)
                return false;

            GetEmptySlot().AddItem(item, amount);
        }
        else
        {
            invenSlot.AddCount(amount);
        }
        return true;
    }

    public InventorySlot SearchItemInInven(Player item)
    {
        return inventorySlots.FirstOrDefault(i => i.item.ar_id == item.ar_id);
    }

    public InventorySlot GetEmptySlot()
    {
        return inventorySlots.FirstOrDefault(i => i.item.ar_id <= -1);
    }

    public bool IsContainItem(ItemObj itemObj)
    {
        return inventorySlots.FirstOrDefault(i => i.item.ar_id == itemObj.itemData.ar_id) != null;
    }

    public void SwapItems(InventorySlot itemA, InventorySlot itemB)
    {
        if (itemA == itemB)
            return;

        if(itemA.GetFlagEquipSlot(itemB.ItemObject) && itemB.GetFlagEquipSlot(itemA.ItemObject))
        {
            InventorySlot temp = new InventorySlot(itemB.item, itemB.itemCnt);
            itemB.UploadSlot(itemA.item, itemA.itemCnt);
            itemA.UploadSlot(temp.item, temp.itemCnt);
        }
    }

    // 사용한 아이템 이벤트 발동
    public Action<ItemObj> OnUseItemObject;

    public void Clear()
    {
        inventory.Clear();
    }
}

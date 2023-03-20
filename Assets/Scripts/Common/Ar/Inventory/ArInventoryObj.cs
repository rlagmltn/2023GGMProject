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

[CreateAssetMenu(fileName ="New ArInventoryObject", menuName ="Ars/ArInventoryObject")]
public class ArInventoryObj : ScriptableObject
{
    public ArDBObj arDBObj;
    public InterfaceType type;

    [SerializeField]
    private ArInventory inventory = new ArInventory();
    public ArInventorySlot[] inventorySlots => inventory.inventorySlots;

    public int GetEmptySlotCount
    {
        get
        {
            int cnt = 0;
            foreach(ArInventorySlot slot in inventorySlots)
            {
                if (slot.item.ar_id <= -1)
                    cnt++;
            }
            return cnt;
        }
    }

    public bool AddItem(Player item, int amount)
    {
        ArInventorySlot invenSlot = SearchItemInInven(item);
        if( !arDBObj.arObjects[item.ar_id].flagStackable || invenSlot == null)
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

    public ArInventorySlot SearchItemInInven(Player item)
    {
        return inventorySlots.FirstOrDefault(i => i.item.ar_id == item.ar_id);
    }

    public ArInventorySlot GetEmptySlot()
    {
        return inventorySlots.FirstOrDefault(i => i.item.ar_id <= -1);
    }

    public bool IsContainItem(ArObj arObj)
    {
        return inventorySlots.FirstOrDefault(i => i.item.ar_id == arObj.arData.ar_id) != null;
    }

    public void SwapItems(ArInventorySlot itemA, ArInventorySlot itemB)
    {
        if (itemA == itemB)
            return;

        if(itemA.GetFlagEquipSlot(itemB.ArObject) && itemB.GetFlagEquipSlot(itemA.ArObject))
        {
            ArInventorySlot temp = new ArInventorySlot(itemB.item, itemB.itemCnt);
            itemB.UploadSlot(itemA.item, itemA.itemCnt);
            itemA.UploadSlot(temp.item, temp.itemCnt);
        }
    }

    // 사용한 아이템 이벤트 발동
    public Action<ArObj> OnUseItemObject;

    public void Clear()
    {
        inventory.Clear();
    }
}

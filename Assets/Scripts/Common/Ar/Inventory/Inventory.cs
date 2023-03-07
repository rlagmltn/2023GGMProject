using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Inventory
{
    public InventorySlot[] inventorySlots = new InventorySlot[24];

    public void Clear()
    {
        foreach(InventorySlot invenSlot in inventorySlots )
        {
            invenSlot.DestoryItem();
        }
    }

    public bool GetFlagHave(int id)
    {
        return inventorySlots.FirstOrDefault(i => i.item.ar_id == id) != null;
    }

    public bool GetFlagHave(ItemObj itemObj)
    {
        return GetFlagHave(itemObj.itemData.ar_id);
    }
}

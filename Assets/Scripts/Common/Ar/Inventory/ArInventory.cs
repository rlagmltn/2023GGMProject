using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ArInventory
{
    public ArInventorySlot[] inventorySlots = new ArInventorySlot[24];

    public void Clear()
    {
        foreach(ArInventorySlot invenSlot in inventorySlots )
        {
            invenSlot.DestoryItem();
        }
    }

    public bool GetFlagHave(int id)
    {
        return inventorySlots.FirstOrDefault(i => i.item.ar_id == id) != null;
    }

    public bool GetFlagHave(ArObj itemObj)
    {
        return GetFlagHave(itemObj.arData.ar_id);
    }
}

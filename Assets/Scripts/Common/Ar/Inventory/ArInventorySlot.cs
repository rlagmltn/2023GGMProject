using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArInventorySlot
{
    // slot에 들어갈 아이템 타입
    public ItemType[] itemTypes = new ItemType[0];

    [NonSerialized]
    public ArInventoryObj arInventoryObj;

    [NonSerialized]
    public GameObject slotUI;

    [NonSerialized]
    public Action<ArInventorySlot> OnPreUpload;

    [NonSerialized]
    public Action<ArInventorySlot> OnPostUpload;

    public Player item;
    public int itemCnt;

    public ArObj ArObject
    {
        get
        {
            return item.ar_id >= 0 ? arInventoryObj.arDBObj.arObjects[item.ar_id] : null;
        }
    }

    public ArInventorySlot() => UploadSlot(new Player(), 0);
    public ArInventorySlot(Player item, int cnt) => UploadSlot(item, cnt);
    public void DestoryItem() => UploadSlot(new Player(), 0);
    public void AddCount(int value) => UploadSlot(item, itemCnt += value);
    public void AddItem(Player item, int cnt) => UploadSlot(item, cnt);

    public void UploadSlot(Player item, int cnt)
    {
        if( cnt <= 0 )
        {
            item = new Player();
        }

        OnPreUpload?.Invoke(this);
        this.item = item;
        this.itemCnt = cnt;
        OnPostUpload?.Invoke(this);
    }

    public bool GetFlagEquipSlot(ArObj itemObj)
    {
        if( itemTypes.Length <= 0 || itemObj == null || itemObj.arData.ar_id < 0 )
        {
            return true;
        }

        foreach(ItemType itemType in itemTypes)
        {
            if (itemType == itemObj.itemType)
                return true;
        }

        return false;
    }
}
